using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.CustomControls.Tabs;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class StudentGroupPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;

        private readonly StudyGroup _group;
        private readonly StudentGroupBlockPrewiew _studentGroupBlockPrewiew;
        private readonly string _studentGroupTitle;

        public StudentGroupPreview(StudentGroupBlockPrewiew studentGroupBlockPrewiew, string studentGroupTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            InitializeComponent();

            _studentGroupTitle = studentGroupTitle;

            CardTitle.DataContext = _studentGroupTitle;
            _studentGroupBlockPrewiew = studentGroupBlockPrewiew;

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

            var studentsData = ProfilePreviewData.GetProfilePreview(_group);
            ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);

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
            JsonBackupManager.SaveCardUserList(_group, _studentGroupTitle);
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
            //TODO: check this
            _studentGroupBlockPrewiew.GroupListPanel.Children.Remove(this);
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            var studentPackBlock = new StudentPackBlockPrewiew(_group, _studentGroupTitle, _navigateService);
            var studentPackTab = new StudentPackTab(studentPackBlock);

            _navigateService.AddToViewList(_studentGroupTitle, studentPackTab);
            _navigateService.OpenView(studentPackTab);
        }
    }
}