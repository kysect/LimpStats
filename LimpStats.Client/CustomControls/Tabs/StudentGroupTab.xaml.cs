using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.Blocks;

namespace LimpStats.Client.CustomControls.Tabs
{
    /// <summary>
    /// Логика взаимодействия для StudentGroupTab.xaml
    /// </summary>
    public partial class StudentGroupTab : UserControl
    {
      
        public StudentGroupTab(StudentGroupBlockPreview blockPreview)
        {
            InitializeComponent();
            StudentBlockPanel.Children.Add(blockPreview);
        }

        private void Show_StudentGroupBlock_Prewiew(object sender, RoutedEventArgs e)
        {
            
        }

        private void Show_StudentGroupBlock_Options(object sender, RoutedEventArgs e)
        {

        }

        private void Show_StudentGroupBlock_Settings(object sender, RoutedEventArgs e)
        {

        }
    }
}
