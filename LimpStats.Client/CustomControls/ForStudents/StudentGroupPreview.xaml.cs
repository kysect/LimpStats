using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class StudentGroupPreview : UserControl
    {
        private readonly StudyGroup _group;
        private readonly Grid _stackPanel;
        private readonly StudentGroupBlock _studentGroupBlock;
        private readonly StackPanel _NavigatePanel;

        public StudentGroupPreview(StudentGroupBlock studentGroupBlock, string groupTitle, Grid stackPanel, StackPanel Navigatepanel)
        {
            InitializeComponent();

            DoubleAnimation buttonAnimation = new DoubleAnimation();
            buttonAnimation.From = 0;
            buttonAnimation.To = 100;
            buttonAnimation.Duration = TimeSpan.FromSeconds(50);
            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.From = new Thickness( Margin.Left+10, Margin.Top, Margin.Right-10, Margin.Bottom);
            moveAnimation.To   = Margin;
            moveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            this.BeginAnimation(UserControl.OpacityProperty, buttonAnimation);
            this.BeginAnimation(UserControl.MarginProperty, moveAnimation);
         
            _studentGroupBlock = studentGroupBlock;
            GroupTitle = groupTitle;
            CardTitle.DataContext = groupTitle;
            _stackPanel = stackPanel;
            _NavigatePanel = Navigatepanel;
            JsonBackupManager.SaveCardName(groupTitle);
            _group = JsonBackupManager.LoadCardUserList(groupTitle);

            if (_group == null)
            {
                _group = new StudyGroup
                {
                    UserList = new List<LimpUser>(),
                    ProblemPackList = new List<ProblemPackInfo>
                    {
                        new ProblemPackInfo("A", TaskPackStorage.TasksAGroup),
                        new ProblemPackInfo("B", TaskPackStorage.TasksBGroup)
                    }
                };
            }

            StudentList.SelectionChanged += LimpUserStatistic;
        }

        public string GroupTitle { get; }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            if (Core.Tools.Tools.CheckInternetConnect() == false)
            {
                MessageBox.Show("Internet connection error");
                return;
            }

            Task.Run(() => Update());
        }

        public void Update()
        {
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = false);

            MultiThreadParser.LoadProfiles(_group);
            IEnumerable<ProfilePreviewData> studentsData = new List<ProfilePreviewData>();

            studentsData = ProfilePreviewData.GetProfilePreview(_group);

            ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = true);
        }

        private void LimpUserStatistic(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                if (e.AddedItems[0] is ProfilePreviewData user)
                {
                    MessageBox.Show($"{user.Username} has {user.Points} points.");
                }
            }
        }

        private void AddUserButton_OnClick(object sender, RoutedEventArgs e)
        {
            var userAdding = new UserAddingWindow();
            userAdding.ShowDialog();

            _group.UserList.Add(new LimpUser(userAdding.Username));
            JsonBackupManager.SaveCardUserList(_group, GroupTitle);
        }

        private void ButtonDeleteCard(object sender, RoutedEventArgs e)
        {
            _studentGroupBlock.Panel.Children.Remove(this);
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            var name = CardTitle.DataContext.ToString();
                var f = new StudentPackBlock(_group, name, _stackPanel, _NavigatePanel);
                _studentGroupBlock.Visibility = Visibility.Hidden;
                _stackPanel.Children.Add(f);
                 _NavigatePanel.Children.Add(new NavigateButton(f, _stackPanel, name, _NavigatePanel));
        }
    }
}