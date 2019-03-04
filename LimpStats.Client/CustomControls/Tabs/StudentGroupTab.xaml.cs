using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.CustomControls.BlocksSettings;

namespace LimpStats.Client.CustomControls.Tabs
{
    public partial class StudentGroupTab : UserControl
    {
        private StudentGroupBlockPreview _block;
        public StudentGroupTab(StudentGroupBlockPreview blockPreview)
        {
            InitializeComponent();
            _block = blockPreview;
            StudentBlockPanel.Children.Add(blockPreview);
        }
        //Гыыы костыли
        private void Show_StudentGroupBlock_Prewiew(object sender, RoutedEventArgs e)
        {
            StudentBlockPanel.Children.Clear();
            StudentBlockPanel.Children.Add(_block);
        }

        private void Show_StudentGroupBlock_Options(object sender, RoutedEventArgs e)
        {
            StudentBlockPanel.Children.Clear();
            StudentBlockPanel.Children.Add(new StudentGroupBlockSettings());
        }

        private void Show_StudentGroupBlock_Settings(object sender, RoutedEventArgs e)
        {

        }
    }
}

