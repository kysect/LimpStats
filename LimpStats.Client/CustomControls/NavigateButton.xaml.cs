using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.Blocks;

namespace LimpStats.Client.CustomControls
{
    public partial class NavigateButton : UserControl
    {
        //TODO: Rewrite
        private StudentGroupBlock _block;
        private ResultGridBlock _blockres;
        private StudentPackBlock _blockpack;
        private  Grid _StackPanel;
        private StackPanel _stackPanel;

        public NavigateButton(StudentGroupBlock block, Grid mainWindow, string name, StackPanel panel)
        {
            InitializeComponent();
            _blockpack = null;
            _block = block;
            _StackPanel = mainWindow;
            butt.DataContext = name;
            _stackPanel = panel;
            foreach (NavigateButton but in _stackPanel.Children)
            {
                if (but != this)
                    but.butt.IsEnabled = true;
            }
            butt.IsEnabled = false;
        }
        public NavigateButton(StudentPackBlock block, Grid mainWindow, string name, StackPanel panel)
        {
            InitializeComponent();

            _block = null;
            _blockpack = block;
            _StackPanel = mainWindow;
            butt.DataContext = name;
            _stackPanel = panel;
            foreach (NavigateButton but in _stackPanel.Children)
            {
                if (but != this)
                    but.butt.IsEnabled = true;
            }
            butt.IsEnabled = false;
        }
        public NavigateButton(ResultGridBlock block, Grid mainWindow, string name, StackPanel panel)
        {
            InitializeComponent();

            _block = null;
            _blockres = block;
            _StackPanel = mainWindow;
            butt.DataContext = name;
            _stackPanel = panel;
            foreach (NavigateButton but in _stackPanel.Children)
            {
                if (but != this)
                    but.butt.IsEnabled = true;
            }
            butt.IsEnabled = false;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var c = _StackPanel.Children.OfType<StudentGroupBlock>();
            var k = _StackPanel.Children.OfType<StudentPackBlock>();
            var l = _StackPanel.Children.OfType<ResultGridBlock>();
            foreach (StudentGroupBlock block in c)
            {
                if (block == _block)
                    block.Visibility = Visibility.Visible;
                else
                    block.Visibility = Visibility.Hidden;
            }
            foreach (ResultGridBlock block in l)
            {
                if (block == _blockres)
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
            foreach (NavigateButton but in _stackPanel.Children)
            {
                if (but != this)
                    but.butt.IsEnabled = true;
            }
            butt.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _StackPanel.Children.Remove(_block);
            _StackPanel.Children.Remove(_blockpack);
            _stackPanel.Children.Remove(this);
        }
    }
}
