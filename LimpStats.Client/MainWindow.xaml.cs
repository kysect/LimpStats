using System;
using System.Linq;
using System.Windows;
using LimpStats.Client.CustomControls;

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
            var f = new InitializationCardWindow();
            f.ShowDialog();
        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            Panel.Children.Add(new StudentGroupPreview(Panel.Children.OfType<StudentGroupPreview>().Count(), "Name"));
            PanelViewer.ScrollToRightEnd();
        }
    }
}