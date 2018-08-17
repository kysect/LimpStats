using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LimpStats.Core;
using LimpStats.Model;

namespace LimpStats.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Button btn = new Button();
            btn.Name = "AddUser";
            btn.Width = 200;
            btn.Height = 50;
            btn.Content = "Добавить енота";
            btn.Click += grid_AddUser;
            btn.Margin = new Thickness(664,264,502,454); 
            Panel.Children.Add(btn);
               // < DataGrid x: Name = "grid" Margin = "294,143,872,273" Width = "200" FrozenColumnCount = "3" MinColumnWidth = "50" GridLinesVisibility = "None" Background = "#555555" RowBackground = "#555555" />
           
                DataGrid grid = new DataGrid();
            grid.Name = "grid";
            grid.Margin = new Thickness(294, 143, 872, 273);
            grid.Width = 200;
            grid.FrozenColumnCount = 3;
            grid.MinColumnWidth = 50;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
            grid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            grid.RowBackground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            Panel.Children.Add(grid);

        }


        private void grid_loaded(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += UpdateGrid;
            worker.RunWorkerAsync();


        }
        private void grid_AddUser(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += AddUserToGrid;
            worker.RunWorkerAsync();

        }
        private void UpdateGrid(object sender, DoWorkEventArgs e)
        {
            StudyGroup group = GenerateTemplateGroup();
            MultiThreadParser.LoadProfiles(group.UserList);
            var res = LoadTotalPoints(group);
            List<GridCard> items = new List<GridCard>();
            foreach (var item in res)
            {
                items.Add(new GridCard(item.Key, item.Key, item.Item2));
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                grid.ItemsSource = items;
            });
        }
        private void AddUserToGrid(object sender, DoWorkEventArgs e)
        {
            StudyGroup group = GenerateTemplateGroup();
            group.UserList.Add(new ElimpUser {Login = "Enosha"});
            MultiThreadParser.LoadProfiles(group.UserList);
            var res = LoadTotalPoints(group);
            List<GridCard> items = new List<GridCard>();
            foreach (var item in res)
            {
                items.Add(new GridCard(item.Key, item.Key, item.Item2));
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
         /      grid.ItemsSource = items;
            });
        }
        private static IEnumerable<(string Key, int)> LoadTotalPoints(StudyGroup group)
        {
            var result = group.GetAllPackResult();


            var list = result.SelectMany(l => l)
                .GroupBy(l => l.Username)
                .Select(gr => (gr.Key, gr.Sum(g => g.TotalPoints)))
                .OrderByDescending(t => t.Item2);
            return list;
        }
        public static StudyGroup GenerateTemplateGroup()
        {
            var users = new List<ElimpUser>
            {
                new ElimpUser("Andrey2005"),
                new ElimpUser("DDsov"),
                new ElimpUser("Den4758"),
            };
            var group = new StudyGroup(users);
            group.ProblemPackList.Add(new ProblemPackInfo("A", TaskPackStorage.TasksAGroup, 300));


            return group;
        }

        public static class TaskPackStorage
        {
            public static readonly List<int> TasksAGroup = new List<int>
            {
                5133,
                7401,
            };

        }
    }
}
