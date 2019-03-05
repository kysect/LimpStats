using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.CustomControls.BlocksSettings;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.Tabs
{
    public partial class StudentPackTab : UserControl
    {
        private StudentPackBlockPreview _block;
        private UserGroup _group;
        public StudentPackTab(StudentPackBlockPreview blockPreview, UserGroup group)
        {
            InitializeComponent();
            _block = blockPreview;
            _group = group;
            StudentBlockPanel.Children.Add(blockPreview);
            }

        private void Show_ProblemPackBlock_Options(object sender, RoutedEventArgs e)
        {
            StudentBlockPanel.Children.Clear();
            StudentBlockPanel.Children.Add(new ProblemPackBlockSettings(_group));
        }

        private void Show_ProblemPackBlock_Prewiew(object sender, RoutedEventArgs e)
        {
            StudentBlockPanel.Children.Clear();
            _block.UpdateUi();
            StudentBlockPanel.Children.Add(_block);
        }

        private void Show_ProblemPackBlock_Settings(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
