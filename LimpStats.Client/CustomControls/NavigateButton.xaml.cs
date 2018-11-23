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
        private MainWindow _mainWindow;
        public NavigateButton(StudentGroupBlock block, MainWindow mainWindow)
        {
            InitializeComponent();
            _block = block;
            _mainWindow = mainWindow;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var c = _mainWindow.StackPanel.Children.OfType<StudentGroupBlock>();
            foreach (var block in c)
            {
                if (block._sumVar == _block._sumVar)
                    block.Visibility = Visibility.Visible;
                else
                {
                    block.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
