using System;
using System.Windows;
using LimpStats.Client.CustomControls;
using LimpStats.Client.Services;

namespace LimpStats.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new CustomControls.InitializationCardWinow(param => MessageBox.Show(param + "!"));
            f.ShowDialog();
        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            var group = InstanceGenerator.GenerateTemplateGroup();
            Panel.Children.Add(new StudentGroupPreview(group));
            PanelViewer.ScrollToRightEnd();
        }
    }
}