using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LimpStats.Client.CustomControls
{
    public partial class NavigateButton : UserControl
    {
        private readonly StudentGroupBlock _block;
        private readonly MainWindow _mainWindow;

        public NavigateButton(StudentGroupBlock block, MainWindow mainWindow)
        {
            InitializeComponent();

            _block = block;
            _mainWindow = mainWindow;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var c = _mainWindow.StackPanel.Children.OfType<StudentGroupBlock>();
            foreach (StudentGroupBlock block in c)
            {
                if (block.SumVar == _block.SumVar)
                    block.Visibility = Visibility.Visible;
                else
                    block.Visibility = Visibility.Hidden;
            }
        }
    }
}
