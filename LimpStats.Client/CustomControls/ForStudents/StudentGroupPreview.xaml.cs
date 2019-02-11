using System;
using System.Collections.Generic;
using System.Linq;
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
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class StudentGroupPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;

        private readonly UserGroup _group;
        //TODO: fix typo
        private readonly StudentGroupBlockPrewiew _studentGroupBlockPrewiew;
        private readonly string _studentGroupTitle;
        private readonly Domain _domain;

        public StudentGroupPreview(StudentGroupBlockPrewiew studentGroupBlockPrewiew, string studentGroupTitle, IViewNavigateService navigateService, Domain domain)
        {
            _navigateService = navigateService;
            _domain = domain;

            InitializeComponent();

            _studentGroupTitle = studentGroupTitle;

            CardTitle.DataContext = _studentGroupTitle;
            _studentGroupBlockPrewiew = studentGroupBlockPrewiew;

            JsonBackupManager.SaveCardName(studentGroupTitle);
            _group = JsonBackupManager.LoadCardUserList(studentGroupTitle);

            //TODO: temp solution, remove
            if (_group == null)
            {
                _group = new UserGroup
                {
                    Users = new List<LimpUser>(),
                    ProblemsPacks = new List<ProblemsPack>
                    {
                        //TODO: implement testing factory
                        //new ProblemPackInfo("A", TaskPackStorage.TasksAGroup),
                        //new ProblemPackInfo("B", TaskPackStorage.TasksBGroup)
                    }
                };
            }

            Task.Run(() => k());

            //ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
            //StudentList.SelectionChanged += LimpUserStatistic;
        }
        private void k()
        {
            var studentsData = ProfilePreviewData.GetProfilePreview(_group);

            foreach (var currRes in studentsData)
            {
                ThreadingTools.ExecuteUiThread(() => Panel.Children.Add(new UserResPrewiew(currRes.Username, currRes.Points)));
            }
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

            ThreadingTools.ExecuteUiThread(() => Panel.Children.Clear());
            foreach (var currRes in studentsData)
            {
                ThreadingTools.ExecuteUiThread(() => Panel.Children.Add(new UserResPrewiew(currRes.Username, currRes.Points)));
            }

            //  ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
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
            var userAdding = new UserAddingWindow(_group.Users);
            userAdding.ShowDialog();

                _group.Users.Add(new LimpUser(userAdding.UsernameEolymp, userAdding.UsernameCodeforces, userAdding.Name));
                JsonBackupManager.SaveCardUserList(_group, _studentGroupTitle);
        }

        private void ButtonDeleteCard(object sender, RoutedEventArgs e)
        {
            //TODO: check this
            _studentGroupBlockPrewiew.GroupListPanel.Children.Remove(this);
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            var studentPackBlock = new StudentPackBlockPrewiew(_group, _studentGroupTitle, _navigateService, _domain);
            var studentPackTab = new StudentPackTab(studentPackBlock);

            _navigateService.AddToViewList(_studentGroupTitle, studentPackTab);
            _navigateService.OpenView(studentPackTab);
        }
    }
}