using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using StudentPackBlockPreview = LimpStats.Client.CustomControls.BlocksPrewiew.StudentPackBlockPreview;

namespace LimpStats.Client.CustomControls.Tabs
{
    /// <summary>
    /// Логика взаимодействия для StudentPackTab.xaml
    /// </summary>
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
