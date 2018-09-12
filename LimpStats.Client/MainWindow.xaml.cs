using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using LimpStats.Client.CustomControls;
using LimpStats.Client.Services;
using LimpStats.Model;

namespace LimpStats.Client
{
    public partial class MainWindow : Window
    {
        //TODO: если нет инета, крашится прога
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new InitializationCardWinow(param => MessageBox.Show(param + "!"));
            f.ShowDialog();
        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            //StudentGroupInitializationBlock a = new StudentGroupInitializationBlock();
            //Panel.Children.Add(a);
            
            Panel.Children.Add(new StudentGroupPreview(Panel.Children.OfType<StudentGroupPreview>().Count(), "Name"));
            PanelViewer.ScrollToRightEnd();
        }
    }
}