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
    /// <summary>
    /// Логика взаимодействия для ProblemTasksPrewiew.xaml
    /// </summary>
    public partial class ProblemTasksPrewiew : UserControl
    {
        private readonly StudyGroup _group;
        private readonly StackPanel _stackPanel;
        private readonly StudentPackBlock _studentPackBlock;

        //TODO: А разве в StudentPackBlock не хранится users и packTitle?
        public ProblemTasksPrewiew(StudentPackBlock studentPackBlock, StudyGroup users, string packTitle, StackPanel stackPanel)
        {
            InitializeComponent();

            _studentPackBlock = studentPackBlock;
            _group = users;
            _stackPanel = stackPanel;
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
            _studentPackBlock.Panel.Children.Remove(this);
        }



        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new ResultGridBlock(_group, CardTitle.DataContext.ToString());
            _studentPackBlock.Visibility = Visibility.Hidden;
            _stackPanel.Children.Add(f);

        }
    }
}
