using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.Models;
using LimpStats.Client.Services;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    public partial class StudentGroupPreview : UserControl
    {
        private readonly ProblemPackInfo _pack =
            new ProblemPackInfo("name", InstanceGenerator.TaskPackStorage.TasksAGroup);

        public readonly int Id;
        private StudyGroup _group;

        public StudentGroupPreview(int id, string username)
        {
            InitializeComponent();

            _group = new StudyGroup
            {
                UserList = new List<ElimpUser>(),
                ProblemPackList = new List<ProblemPackInfo> {_pack}
            };
            //TODO
            Id = id;
            Username = username;
            CardButton.Content = Username;
        }

        public string Username { get; }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Update());
        }

        private void Update()
        {
            _group = InstanceGenerator.GenerateTemplateGroup(Id);

            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = false);
            IEnumerable<ProfilePreviewData> studentsData = MainWindowService.LoadProfilePreview(_group);
            ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = true);

            StudentList.SelectionChanged += ElimpUserStatistic;
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
            var f = new InitializationCardWindow();
            f.ShowDialog();
            var user = new ElimpUser(f.LoginTextBox.Text, "enosha");
            JsonBackupManager.SaveToJsonOne(user, Id);
        }

        private void ButtonDeleteCard(object sender, RoutedEventArgs e)
        {
            var a = new MainWindow();
            //TODO: словил ошибку, что не id не найден
            var element = a.Panel.Children.OfType<StudentGroupPreview>().FirstOrDefault(f => f.Id == Id);
            if (element != null)
            {
                a.Panel.Children.Remove(element);
            }
        }
    }
}