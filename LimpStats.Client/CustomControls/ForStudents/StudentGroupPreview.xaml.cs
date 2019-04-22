using System;
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
using System.IO;
using OfficeOpenXml;
using Microsoft.Win32;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class StudentGroupPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;

        private readonly UserGroup _group;
        private readonly string _studentGroupTitle;


        public StudentGroupPreview(string studentGroupTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;


            InitializeComponent();

            _studentGroupTitle = studentGroupTitle;

            CardTitle.DataContext = _studentGroupTitle;
            _group = DataProvider.UserGroupRepository.Read(studentGroupTitle);

            // TODO: Remove?
            if (_group == null)
            {
                _group = new UserGroup
                {
                    Title = studentGroupTitle,
                    Users = new List<LimpUser>(),
                    ProblemsPacks = new List<ProblemsPack>
                    {
                        //new ProblemPackInfo("A", TaskPackStorage.TasksAGroup),
                        //new ProblemPackInfo("B", TaskPackStorage.TasksBGroup)
                    }
                };
                DataProvider.UserGroupRepository.Create(_group);
            }

            Task.Run(() => GetUserProfileData());
            //ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
            //StudentList.SelectionChanged += LimpUserStatistic;
        }
        private static int LongRunningMethod()
        {
            var r = new Random();

            var randomNumber = r.Next(1, 10);

            var delayInMilliseconds = randomNumber * 1000;

            Task.Delay(delayInMilliseconds).Wait();

            return randomNumber;
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
            IEnumerable<ProfilePreviewData> studentsData = ProfilePreviewData.GetProfilePreview(_group);

            ThreadingTools.ExecuteUiThread(() => Panel.Children.Clear());
            foreach (var currRes in studentsData)
            {
                ThreadingTools.ExecuteUiThread(() => Panel.Children.Add(new UserResPrewiew(currRes.Username, currRes.Points)));
            }

            //  ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = true);

            DataProvider.UserGroupRepository.Update(_group);
            //JsonBackupManager.SaveCardUserList(_group, _studentGroupTitle);
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            var studentPackBlock = new StudentPackBlockPreview(_group, _navigateService);
            var studentPackTab = new StudentPackTab(studentPackBlock, _group);

            _navigateService.AddToViewList(_studentGroupTitle, studentPackTab);
            _navigateService.OpenView(studentPackTab);
        }

        private void SaveToExcelButton_Click(object sender, RoutedEventArgs e)
        {
    //        var studentsData = ProfilePreviewData.GetProfilePreview(_group);
            
    //        using (var excel = new ExcelPackage(new FileInfo(SaveFileDialog() + ".xlsx")))
    //        {
    //            var ws = excel.Workbook.Worksheets.Add("StudentGroup");
    //            int i = 1;
    //            foreach (var curRes in studentsData)
    //            {
    //                ws.Cells[i, 1].Value = curRes.Username;
    //                ws.Cells[i, 2].Value = curRes.Points;
    //                i++;
    //            }
    //            excel.Save();
    //        }

        }
       
    }
}