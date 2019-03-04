using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Client.Tools;
using LimpStats.Client.CustomControls.BlocksSettings;
using StudentGroupBlockPreview = LimpStats.Client.CustomControls.BlocksPrewiew.StudentGroupBlockPreview;

namespace LimpStats.Client.CustomControls.Tabs
{
    /// <summary>
    /// Логика взаимодействия для StudentGroupTab.xaml
    /// </summary>
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

