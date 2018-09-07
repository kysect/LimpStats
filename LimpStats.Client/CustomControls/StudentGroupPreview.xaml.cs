using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.Models;
using LimpStats.Client.Services;
using LimpStats.Client.Tools;
using LimpStats.Core;
using LimpStats.Model;
using Newtonsoft.Json;
namespace LimpStats.Client.CustomControls
{
    public partial class StudentGroupPreview : UserControl
    {
        private  StudyGroup _group;
        public int Id;
        private string Name;
        private ProblemPackInfo pack = new ProblemPackInfo("name", InstanceGenerator.TaskPackStorage.TasksAGroup);
        public StudentGroupPreview(int id, string name)
        {
            _group = new StudyGroup();
            _group.UserList = new List<ElimpUser>();
            //TODO
            _group.ProblemPackList = new List<ProblemPackInfo>{pack};
            Id = id;
            Name = name;
            InitializeComponent();
            CardButton.Content = Name;
        }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Update());
        }

        private void Update()
        {
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = false);
            _group = InstanceGenerator.GenerateTemplateGroup(Id);
            var studentsData = MainWindowService.LoadProfilePreview(_group);
            ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = true);

            StudentList.SelectionChanged += ElimpUserStatistic;

        }
        private void ElimpUserStatistic(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                if (e.AddedItems[0] is ProfilePreviewData user)
                    MessageBox.Show($"{user.Username} has {user.Points} points.");
            }
        }

        private void AddUserButton_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new InitializationCardWinow(param => MessageBox.Show(param + "!"));
            f.ShowDialog();
            ElimpUser user = new ElimpUser(f.textBox1.Text, "enosha");
                Database.JsonBackupManager.SaveToJsonOne(user, Id);
        }
    }
}