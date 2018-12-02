using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LimpStats.Client.CustomControls
{
    public partial class NavigateButton : UserControl
    {
        private  StudentGroupBlock _block;
        private  StudentPackBlock _blockpack;
        private  Grid _StackPanel;

        public NavigateButton(StudentGroupBlock block, Grid mainWindow)
        {
            InitializeComponent();
            _blockpack = null;
            _block = block;
            _StackPanel = mainWindow;
        }
        public NavigateButton(StudentPackBlock block, Grid mainWindow)
        {
            InitializeComponent();
            _block = null;
            _blockpack = block;
            _StackPanel = mainWindow;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var c = _StackPanel.Children.OfType<StudentGroupBlock>();
            var k = _StackPanel.Children.OfType<StudentPackBlock>();
            foreach (StudentGroupBlock block in c)
            {
                if (block == _block)
                    block.Visibility = Visibility.Visible;
                else
                    block.Visibility = Visibility.Hidden;
            }
            foreach (StudentPackBlock block in k)
            {
                if (block == _blockpack)
                    block.Visibility = Visibility.Visible;
                else
                    block.Visibility = Visibility.Hidden;
            }
        }
    }
}
