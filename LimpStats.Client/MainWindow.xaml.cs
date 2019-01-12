using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.Tools;
using LimpStats.Database;

namespace LimpStats.Client
{
    public partial class MainWindow : Window, IViewNavigateService
    {
        private StudentGroupBlock _block;
        public MainWindow()
        {
            InitializeComponent();
            var studentGroupBlock = new StudentGroupBlock(this);
            OpenView(studentGroupBlock);
            _block = studentGroupBlock;
        }

        public void RemoveButton(NavigateButton button)
        {
            NavigatePanel.Children.Remove(button);
            if (MainWindowContent.Content == button.CurrentControl.Content)
            {
                MainWindowContent.Visibility = Visibility.Hidden;
            }
            //TODO: implement closing content window
        }

        public void AddToViewList(string viewTitle, UserControl view)
        {
            NavigatePanel.Children.Add(new NavigateButton(viewTitle, this, view));
        }

        public void OpenView(UserControl view)
        {
            MainWindowContent.Visibility = Visibility.Visible;
            MainWindowContent.Content = view.Content;
        }

        private void ButtonHome_OnClick(object sender, RoutedEventArgs e)
        {
            OpenView(_block);
        }

        private void ButtonCleanCache_OnClick(object sender, RoutedEventArgs e)
        {
            JsonBackupManager.ClearCache();
        }
    }
}