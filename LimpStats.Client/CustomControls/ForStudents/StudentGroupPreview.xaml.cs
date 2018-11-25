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
using LimpStats.Database.Models;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    public partial class StudentGroupPreview : UserControl
    {
        private readonly StudyGroup _group;
        private readonly Grid _stackPanel;
        private readonly StudentGroupBlock _studentGroupBlock;

        public StudentGroupPreview(StudentGroupBlock studentGroupBlock, string groupTitle, Grid stackPanel)
        {
            InitializeComponent();
            _studentGroupBlock = studentGroupBlock;
            GroupTitle = groupTitle;
            CardTitle.Content = groupTitle;
            _stackPanel = stackPanel;

            JsonBackupManager.SaveCardName(groupTitle);
            _group = JsonBackupManager.LoadCardUserList(GroupTitle);

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

        public string GroupTitle { get; }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            if (ConnectionAvailable("https://www.google.com") == false)
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
            var f = new InitializationCardWindow(_group, GroupTitle);
            f.ShowDialog();
        }

        private void ButtonDeleteCard(object sender, RoutedEventArgs e)
        {
            _studentGroupBlock.Panel.Children.Remove(this);
        }

        //TODO: вынести в .Core.Tools
        public bool ConnectionAvailable(string strServer)
        {
            try
            {
                var reqFP = (HttpWebRequest) WebRequest.Create(strServer);

                var rspFP = (HttpWebResponse) reqFP.GetResponse();
                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    rspFP.Close();
                    return true;
                }

                rspFP.Close();
                return false;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
                var f = new StudentPackBlock(_group, CardTitle.Content.ToString());
                _studentGroupBlock.Visibility = Visibility.Hidden;
                _stackPanel.Children.Add(f);
        }
    }
}