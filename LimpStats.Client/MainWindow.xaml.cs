using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using LimpStats.Client.Services;
using LimpStats.Core;
using LimpStats.Model;

namespace LimpStats.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _gridid;
        private Button _presbutton;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGrid(object sender, DoWorkEventArgs e)
        {
            //<Button x:Name="RefreshAllResults" Content="Обновить" Click="grid_loaded" Margin="294,88,872,630" Width="200" Height="50" />
            Application.Current.Dispatcher.Invoke(() =>
            {
                double xLeftShift = 0;
                //double xRightShift = 0;
                if (_gridid != 0)
                {
                    xLeftShift = Panel.Children?.OfType<ListBox>()?.Last().Margin.Left ?? 600;
                }
                else
                {
                    xLeftShift = -150;
                }

                var btn = UiElementsGenerator.CreateButton(_gridid, xLeftShift);
                btn.Click += Grid_loaded;
                //btn.Margin = new Thickness(xLeftShift + 50, 200, panel.Margin.Right-xLeftShift, 200);
                Panel.Children.Add(btn);

                var grid = UiElementsGenerator.CreateDataGrid(_gridid, xLeftShift);
                Panel.Children.Add(grid);
                _gridid++;
            });
        }

        private void Grid_loaded(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            _presbutton = sender as Button;
            worker.DoWork += UpdateGrid;
            worker.RunWorkerAsync();
        }

        private void Grid_newgrid(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += NewGrid;
            worker.RunWorkerAsync();
            // newGrid(1);
        }

        private void Grid_AddUser(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            _presbutton = sender as Button;
            worker.DoWork += AddUserToGrid;
            worker.RunWorkerAsync();
        }

        private void UpdateGrid(object sender, DoWorkEventArgs e)
        {
            //StudyGroup group = GenerateTemplateGroup();
            //MultiThreadParser.LoadProfiles(group.UserList);
            //var res = LoadTotalPoints(group);
            //List<GridCard> items = new List<GridCard>();
            //foreach (var item in res)
            //{
            //    items.Add(new GridCard(item.Key, item.Key, item.Item2));
            //}
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    panel.Children.OfType<DataGrid>().First(a => a.Name == (string)presbutton.DataContext).ItemsSource = items;
            //});
            Application.Current.Dispatcher.Invoke(() =>
            {
                //MultiThreadParser.LoadProfiles(group.UserList);
                //var res = LoadTotalPoints(group);
                //List<GridCard> items = new List<GridCard>();
                var group = InstanceGenerator.GenerateTemplateGroup();
                var res = group
                    .UserList
                    .OrderByDescending(u => u.CompletedTaskCount());

                var items = new List<Button>();
                foreach (var item in res)
                {
                    var btn = new Button
                    {
                        Name = "Button",
                        Width = 200,
                        Height = 50,
                        DataContext = $"{item.Login}",
                        Content = $"{item.Login,30} | {item.CompletedTaskCount()}"
                    };
                    btn.Click += Grid_loaded;
                    //btn.Margin = new Thickness(xLeftShift + 50, 200, panel.Margin.Right-xLeftShift, 200);
                    //btn.Margin = new Thickness(xLeftShift + 250, 100, 0, 0);
                    //presbutton.DataContext .Children.Add(btn);
                    //items.Add(new GridCard(item.Key, item.Key, item.Item2));
                    items.Add(btn);
                }

                Panel.Children
                        .OfType<ListBox>()
                        .First(a => a.Name == (string) _presbutton.DataContext)
                        .ItemsSource = items;
            });
        }

        private void AddUserToGrid(object sender, DoWorkEventArgs e)
        {
            var cards = MainWindowService.GetCardWithEnosha();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Panel.Children
                    .OfType<DataGrid>()
                    .First(a => a.Name == (string)_presbutton.DataContext)
                    .ItemsSource = cards;
            });
        }
    }
}