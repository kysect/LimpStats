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

namespace LimpStats.Client.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для NavigateButton.xaml
    /// </summary>
    public partial class NavigateButton : UserControl
    {
        private StudentGroupBlock _block;
        public NavigateButton(StudentGroupBlock block)
        {
            InitializeComponent();
            _block = block;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_block.Visibility == Visibility.Visible)
                _block.Visibility = Visibility.Hidden;
            else
                _block.Visibility = Visibility.Visible;
        }
    }
}
