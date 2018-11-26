using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Database.Models;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    /// <summary>
    /// Логика взаимодействия для ProblemTasksPrewiew.xaml
    /// </summary>
    public partial class ProblemTasksPrewiew : UserControl
    {
        private readonly StudyGroup _group;
        private readonly Grid _stackPanel;
        private readonly StudentPackBlock _studentPackBlock;
        public ProblemTasksPrewiew(StudentPackBlock studentPackBlock, StudyGroup users, string packTitle)
        {
            InitializeComponent();
            _studentPackBlock = studentPackBlock;
            _group = users;
            GroupTitle = packTitle;
            CardTitle.Content = packTitle;
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
            if (Core.Tools.Tools.ConnectionAvailable("https://www.google.com") == false)
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
            //TODO:
        }
    }
}
