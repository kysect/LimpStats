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
using LimpStats.Client.CustomControls.Blocks;

namespace LimpStats.Client.CustomControls.Tabs
{
    /// <summary>
    /// Логика взаимодействия для StudentGroupTab.xaml
    /// </summary>
    public partial class StudentGroupTab : UserControl
    {
      
        public StudentGroupTab(StudentGroupBlockPrewiew blockPrewiew)
        {
            InitializeComponent();
            StudentBlockPanel.Children.Add(blockPrewiew);
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
