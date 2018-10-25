using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    public partial class StudentGroupPreview : UserControl
    {
        public int Id { get; }

        private static int _totalCount = 0;
        private readonly StudyGroup _group;
        private readonly MainWindow _window;

        public StudentGroupPreview(MainWindow mainWindow, string groupTitle)
        {
            InitializeComponent();
            
            Id = _totalCount;
            GroupTitle = groupTitle;
            CardTitle.Content = GroupTitle;
            _window = mainWindow;
            _totalCount++;

            _group = JsonBackupManager.LoadCardUserList(GroupTitle);
            if (_group == null)
            {
                _group = new StudyGroup
                {
                    UserList = new List<LimpUser>(),
                    ProblemPackList = new List<ProblemPackInfo> { new ProblemPackInfo("name", TaskPackStorage.TasksAGroup) }
                };
            }

            StudentList.SelectionChanged += ElimpUserStatistic;
        }

        public string GroupTitle { get; }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            if (ConnectionAvailable("https://www.google.com") == false)
            {
                MessageBox.Show("Internet connection error");
                return;
            }
            Task.Run(() => Update());
            JsonBackupManager.SaveCardUserList(_group, GroupTitle);
        }

        public void Update()
        {
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = false);

            MultiThreadParser.LoadProfiles(_group);
            IEnumerable<ProfilePreviewData> studentsData = ProfilePreviewData.GetProfilePreview(_group);
            ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);

            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = true);
        }

        private void ElimpUserStatistic(object sender, SelectionChangedEventArgs e)
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
            var f = new InitializationCardWindow(_group);
            f.ShowDialog();
        }

        private void ButtonDeleteCard(object sender, RoutedEventArgs e)
        {
            var element = _window.Panel.Children.OfType<StudentGroupPreview>().FirstOrDefault(f => f.Id == Id);
            if (element != null)
            {
                _window.Panel.Children.Remove(element);
            }
        }
        public bool ConnectionAvailable(string strServer)
        {
            try
            {
                HttpWebRequest reqFP = (HttpWebRequest)HttpWebRequest.Create(strServer);

                HttpWebResponse rspFP = (HttpWebResponse)reqFP.GetResponse();
                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    rspFP.Close();
                    return true;
                }
                else
                {
                    rspFP.Close();
                    return false;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}