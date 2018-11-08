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
        private readonly StudentGroupBlock _studentGroupBlock;
        private readonly SumVar _sumVar;

        public StudentGroupPreview(StudentGroupBlock studentGroupBlock, string groupTitle, SumVar sumVar)
        {
            InitializeComponent();
            
            Id = _totalCount;
            GroupTitle = groupTitle;
            CardTitle.Content = GroupTitle;
            _studentGroupBlock = studentGroupBlock;
            _totalCount++;
            _sumVar = sumVar;
            JsonBackupManager.SaveCardName(groupTitle);
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

        public StudentGroupPreview(StudentGroupBlock studentGroupBlock, StudyGroup users, string packTitle)
        {
            InitializeComponent();

            Id = _totalCount;
            GroupTitle = packTitle;
            CardTitle.Content = GroupTitle;
            _studentGroupBlock = studentGroupBlock;
            _totalCount++;
            _sumVar = SumVar.Pack;
            _group = users;
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
            IEnumerable<ProfilePreviewData> studentsData = new List<ProfilePreviewData>();
            if (_sumVar == SumVar.AllPack)
            {
                studentsData = ProfilePreviewData.GetProfilePreview(_group);
            }
            else
            {
               studentsData = ProfilePreviewData.GetProfilePackPreview(_group, CardTitle.Content.ToString());
            }
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
            var element =  _studentGroupBlock.Panel.Children.OfType<StudentGroupPreview>().FirstOrDefault(f => f.Id == Id);
            if (element != null)
            {
                _studentGroupBlock.Panel.Children.Remove(element);
                JsonBackupManager.DeleteCard(element.GroupTitle);
            }
        }
        public bool ConnectionAvailable(string strServer)
        {
            try
            {
                HttpWebRequest reqFP = (HttpWebRequest)WebRequest.Create(strServer);

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

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            var f = new StudentGroupBlock(SumVar.Pack, CardTitle.Content.ToString());
            var k = (StackPanel)_studentGroupBlock.Panel;
            k.Children.Add(f);
        }
    }
}