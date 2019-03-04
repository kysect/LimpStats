using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.BlocksPrewiew;

namespace LimpStats.Client.CustomControls.Tabs
{
    public partial class StudentPackTab : UserControl
    {

        public StudentPackTab(StudentPackBlockPreview blockPreview)
        {
            InitializeComponent();
            StudentBlockPanel.Children.Add(blockPreview);
        }

        private void Show_StudentPackBlock_Prewiew(object sender, RoutedEventArgs e)
        {

        }

        private void Show_StudentPackBlock_Options(object sender, RoutedEventArgs e)
        {

        }

        private void Show_StudentPackBlock_Settings(object sender, RoutedEventArgs e)
        {

        }
    }
}
