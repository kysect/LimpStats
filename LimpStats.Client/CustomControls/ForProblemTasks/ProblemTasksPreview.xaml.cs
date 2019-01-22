using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    public partial class ProblemTasksPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;

        private readonly StudyGroup _group;
        private readonly StudentPackBlockPrewiew _studentPackBlockPrewiew;

        public ProblemTasksPreview(StudentPackBlockPrewiew studentPackBlockPrewiew, StudyGroup users, string packTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            InitializeComponent();

            _studentPackBlockPrewiew = studentPackBlockPrewiew;
            _group = users;
            PackTitle = packTitle;
            CardTitle.DataContext = packTitle;
            //if (_group == null)
            //{
            //    _group = new StudyGroup
            //    {
            //        UserList = new List<LimpUser>(),
            //        ProblemPackList = new List<ProblemPackInfo>
            //        {
            //            new ProblemPackInfo(PackTitle, TaskPackStorage.TasksAGroup)
            //        }
            //    };
            //}
            var studentsData = ProfilePreviewData.GetProfilePackPreview(_group, packTitle);
            ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);

        }

        public string PackTitle { get; }

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

            studentsData = ProfilePreviewData.GetProfilePackPreview(_group, PackTitle);

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


        private void ButtonDeleteCard(object sender, RoutedEventArgs e)
        {
            //TODO: check this
            _studentPackBlockPrewiew.PackListPanel.Children.Remove(this);
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            string packTitle = CardTitle.DataContext.ToString();
            ProblemPackInfo pack = _group.ProblemPackList.Find(p => p.PackTitle == packTitle);
            var resultGridBlock = new ResultGridBlock(_group.UserList, pack);

            _navigateService.AddToViewList(CardTitle.DataContext.ToString(), resultGridBlock);
            _navigateService.OpenView(resultGridBlock);

        }
    }
}
