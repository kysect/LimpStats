using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.Services;
using LimpStats.Client.Tools;
using LimpStats.Core;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    /// <summary>
    ///     Interaction logic for StudentGroupPreview.xaml
    /// </summary>
    public partial class StudentGroupPreview : UserControl
    {
        public StudentGroupPreview()
        {
            InitializeComponent();
        }

        private void ButtonClick_Update(object sender, RoutedEventArgs e)
        {
            var group = DataContext as StudyGroup;
            Task.Run(() => Update(group));
        }

        private void Update(StudyGroup group)
        {
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = false);
            if (group == null)
            {
                group = InstanceGenerator.GenerateTemplateGroup();
            }

            MultiThreadParser.LoadProfiles(group.UserList);

            var studentsData = MainWindowService
                .LoadTotalPoints(group)
                .Select(res => $"{res.Username} [{res.Points}]");
            ThreadingTools.ExecuteUiThread(() => StudentList.ItemsSource = studentsData);
            ThreadingTools.ExecuteUiThread(() => UpdateButton.IsEnabled = true);
            //TODO
            StudentList.SelectionChanged += ElimpUserStatistic;

        }
        private void ElimpUserStatistic(object sender, SelectionChangedEventArgs e)
        {
            var group = DataContext as StudyGroup;
            //TODO
            MessageBox.Show(e.AddedItems[0].ToString());
        }
    }
}