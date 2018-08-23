using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.Models;
using LimpStats.Client.Services;
using LimpStats.Client.Tools;
using LimpStats.Core;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    public partial class StudentGroupPreview : UserControl
    {
        private readonly StudyGroup _group;
        public int Id;
        private string Name;
        public StudentGroupPreview(StudyGroup group, int id, string name)
        {
            _group = group;
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
            _group.LoadProfiles();
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
    }
}