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
using System.Windows.Shapes;
using LimpStats.Client.Services;

namespace LimpStats.Client.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для StudentGroupInitializationBlock.xaml
    /// </summary>
    public partial class StudentGroupInitializationBlock : UserControl
    {
        public StudentGroupInitializationBlock()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            a.OnClick_UpdatePanel(sender, e);
        }
    }
}
