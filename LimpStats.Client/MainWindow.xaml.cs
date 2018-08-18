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
        private Button presbutton;
        private int gridid = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newGrid(object sender, DoWorkEventArgs e)
        {
            //<Button x:Name="RefreshAllResults" Content="Обновить" Click="grid_loaded" Margin="294,88,872,630" Width="200" Height="50" />
            Application.Current.Dispatcher.Invoke(() =>
            {
            double xLeftShift = 0;
            double xRightShift = 0;
            if (gridid != 0)
            {
                    xLeftShift = panel.Children?.OfType<DataGrid>()?.Last().Margin.Left ?? 600;
            } // < DataGrid x: Name = "grid" Margin = "294,143,872,273" Width = "200" FrozenColumnCount = "3" MinColumnWidth = "50" GridLinesVisibility = "None" Background = "#555555" RowBackground = "#555555" />
            else
            {
                xLeftShift  = -150;
            }
                Button btn = new Button();
                btn.Name = "Refresh";
                btn.Width = 200;
                btn.Height = 50;
                btn.DataContext = $"grid{gridid}";
                btn.Content = "Refresh";
                btn.Click += grid_loaded;
                //btn.Margin = new Thickness(xLeftShift + 50, 200, panel.Margin.Right-xLeftShift, 200);
                btn.Margin = new Thickness(xLeftShift + 250, 100, 0, 0);
                panel.Children.Add(btn);

                DataGrid grid = new DataGrid();
                grid.Name = $"grid{gridid}";
                grid.Margin = new Thickness(xLeftShift + 250, 150, 0, 0);
                grid.Width = 200;
                grid.Height = 100;
                grid.FrozenColumnCount = 3;
                grid.MinColumnWidth = 50;
                grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
                grid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                grid.RowBackground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                panel.Children.Add(grid);
            gridid++;
            });
        }
        private void grid_loaded(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            presbutton = sender as Button;
            worker.DoWork += UpdateGrid;
            worker.RunWorkerAsync();


        }
        private void grid_newgrid(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += newGrid;
            worker.RunWorkerAsync();
            // newGrid(1);
        }
        private void grid_AddUser(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            presbutton = sender as Button;
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
                panel.Children.OfType<DataGrid>().First(a => a.Name == (string)presbutton.DataContext).ItemsSource = items;
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
                panel.Children.OfType<DataGrid>().First(a => a.Name == presbutton.DataContext).ItemsSource = items;
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
