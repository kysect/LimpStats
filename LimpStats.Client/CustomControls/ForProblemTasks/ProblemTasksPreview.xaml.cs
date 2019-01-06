using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    public partial class ProblemTasksPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;

        private readonly StudyGroup _group;
        private readonly StudentPackBlock _studentPackBlock;

        //TODO: А разве в StudentPackBlock не хранится users и packTitle?
        public ProblemTasksPreview(StudentPackBlock studentPackBlock, StudyGroup users, string packTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            InitializeComponent();

            _studentPackBlock = studentPackBlock;
            _group = users;
            GroupTitle = packTitle;
            CardTitle.DataContext = packTitle;
            if (_group == null)
            {
                _group = new StudyGroup
                {
                    UserList = new List<LimpUser>(),
                    ProblemPackList = new List<ProblemPackInfo>
                    {
                        new ProblemPackInfo(packTitle, TaskPackStorage.TasksAGroup)
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

            studentsData = ProfilePreviewData.GetProfilePackPreview(_group, GroupTitle);

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
            //TODO:
        }

        private void ButtonDeleteCard(object sender, RoutedEventArgs e)
        {
            //TODO: check this
            _studentPackBlock.PackListPanel.Children.Remove(this);
        }



        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            var resultGridBlock = new ResultGridBlock(_group, CardTitle.DataContext.ToString());
            //TODO: check this
            _studentPackBlock.Visibility = Visibility.Hidden;

            _navigateService.AddToViewList(CardTitle.DataContext.ToString(), resultGridBlock);
            _navigateService.OpenView(resultGridBlock);

        }
    }
}
