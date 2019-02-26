using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.CustomControls.Tabs;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;
using Domain = LimpStats.Client.Models.Domain;
using StudentGroupBlockPreview = LimpStats.Client.CustomControls.BlocksPrewiew.StudentGroupBlockPreview;
using StudentPackBlockPreview = LimpStats.Client.CustomControls.BlocksPrewiew.StudentPackBlockPreview;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class StudentGroupPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;

        private readonly UserGroup _group;
        private readonly string _studentGroupTitle;
        private readonly Domain _domain;

        public StudentGroupPreview(string studentGroupTitle, IViewNavigateService navigateService, Domain domain)
        {
            _navigateService = navigateService;
            _domain = domain;

            InitializeComponent();

            _studentGroupTitle = studentGroupTitle;

            CardTitle.DataContext = _studentGroupTitle;
            //TODO:
            DataProvider.UserGroupRepository.Read(studentGroupTitle);

            //JsonBackupManager.SaveCardName(studentGroupTitle);
            //_group = JsonBackupManager.LoadCardUserList(studentGroupTitle);

            //TODO: temp solution, remove
            if (_group == null)
            {
                _group = new UserGroup
                {
                    Title = studentGroupTitle,
                    Users = new List<LimpUser>(),
                    ProblemsPacks = new List<ProblemsPack>
                    {
                        //TODO: implement testing factory
                        //new ProblemPackInfo("A", TaskPackStorage.TasksAGroup),
                        //new ProblemPackInfo("B", TaskPackStorage.TasksBGroup)
                    }
                };
            }

            //TODO: Что по неймингу?)
            Task.Run(() => GetUserProfileData());

            //ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
            //StudentList.SelectionChanged += LimpUserStatistic;
        }
        private void GetUserProfileData()
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

            //TODO:
            DataProvider.UserGroupRepository.Update(_group);
            //JsonBackupManager.SaveCardUserList(_group, _studentGroupTitle);
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
                //TODO:
                DataProvider.UserGroupRepository.Update(_group);
                //JsonBackupManager.SaveCardUserList(_group, _studentGroupTitle);
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            var studentPackBlock = new StudentPackBlockPreview(_group, _navigateService, _domain);
            var studentPackTab = new StudentPackTab(studentPackBlock);

            _navigateService.AddToViewList(_studentGroupTitle, studentPackTab, _domain);
            _navigateService.OpenView(studentPackTab);
        }
    }
}