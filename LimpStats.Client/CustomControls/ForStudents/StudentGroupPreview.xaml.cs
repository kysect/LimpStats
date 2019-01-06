using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
        private readonly StackPanel _navigatePanel;
        private readonly string _studentGroupTitle;

        public StudentGroupPreview(StudentGroupBlock studentGroupBlock, string studentGroupTitle, Grid stackPanel, StackPanel navigatePanel)
        {
            InitializeComponent();

            _studentGroupTitle = studentGroupTitle;

            CardTitle.DataContext = _studentGroupTitle;
            _studentGroupBlock = studentGroupBlock;
            _stackPanel = stackPanel;
            _navigatePanel = navigatePanel;

            //TODO: remove saving?
            JsonBackupManager.SaveCardName(studentGroupTitle);
            _group = JsonBackupManager.LoadCardUserList(studentGroupTitle);

            AddAnimation();

            //TODO: temp solution, remove
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

        private void AddAnimation()
        {
            var buttonAnimation = new DoubleAnimation
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromSeconds(50)
            };

            var moveAnimation = new ThicknessAnimation
            {
                From = new Thickness(Margin.Left + 10, Margin.Top, Margin.Right - 10, Margin.Bottom),
                To = Margin,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            BeginAnimation(OpacityProperty, buttonAnimation);
            BeginAnimation(MarginProperty, moveAnimation);
        }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            if (Core.Tools.Tools.CheckInternetConnect() == false)
            {
                MessageBox.Show("Internet connection error");
                return;
            }

            Task.Run(() => Update());
        }

        private void Update()
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
            JsonBackupManager.SaveCardUserList(_group, _studentGroupTitle);
        }

        private void ButtonDeleteCard(object sender, RoutedEventArgs e)
        {
            _studentGroupBlock.Panel.Children.Remove(this);
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new StudentPackBlock(_group, _studentGroupTitle, _stackPanel, _navigatePanel);
            _studentGroupBlock.Visibility = Visibility.Hidden;
            _stackPanel.Children.Add(f);
            _navigatePanel.Children.Add(new NavigateButton(f, _stackPanel, _studentGroupTitle, _navigatePanel));
        }
    }
}