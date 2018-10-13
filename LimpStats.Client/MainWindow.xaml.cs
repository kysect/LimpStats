using System;
using System.Linq;
using System.Windows;
using LimpStats.Client.CustomControls;
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

        private void Button1_Click(object sender, EventArgs e)
        {
            var f = new InitializationCardWindow(new StudyGroup());
            f.ShowDialog();
        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            Panel.Children.Add(new StudentGroupPreview(this, "Name"));
            PanelViewer.ScrollToRightEnd();
        }

        private void LoadFromFile_ButtonClick(object sender, RoutedEventArgs e)
        {
            var preview = new StudentGroupPreview(this, FilePath.Text);
            preview.Update();
            Panel.Children.Add(preview);
            PanelViewer.ScrollToRightEnd();
        }
    }
}