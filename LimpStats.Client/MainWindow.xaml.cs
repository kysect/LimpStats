using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using LimpStats.Client.CustomControls;
using LimpStats.Client.Services;
using LimpStats.Client.Tools;
using LimpStats.Core;
using LimpStats.Model;

namespace LimpStats.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            InitializationCardWinow f = new InitializationCardWinow((param) => MessageBox.Show(param + "!"));
            f.ShowDialog();
        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            Panel.Children.Add(new StudentGroupPreview());
            Panel.Width += 200;
            ScrollViewer.ScrollToRightEnd();
        }

        //private void Grid_AddUser(object sender, RoutedEventArgs e)
        //{
        //    var worker = new BackgroundWorker();
        //    _presbutton = sender as Button;
        //    worker.DoWork += AddUserToGrid;
        //    worker.RunWorkerAsync();
        //}

        //private void AddUserToGrid(object sender, DoWorkEventArgs e)
        //{
        //    var cards = MainWindowService.GetCardWithEnosha();

        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        Panel.Children
        //            .OfType<DataGrid>()
        //            .First(a => a.Name == (string)_presbutton.DataContext)
        //            .ItemsSource = cards;
        //    });
        //}
    }
}