using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.CustomControls.Tabs;
using LimpStats.Client.Tools;
using LimpStats.Database;

namespace LimpStats.Client
{
    public partial class MainWindow : Window, IViewNavigateService
    {
        private readonly StudentGroupTab _tab;
        public MainWindow()
        {
            InitializeComponent();
    
            var studentGroupBlockEolymp = new StudentGroupBlockPreview(this);
            var studentGroupTabEolymp = new StudentGroupTab(studentGroupBlockEolymp);
            OpenView(studentGroupTabEolymp);
            _tab = studentGroupTabEolymp;
        }

        public void RemoveButton(NavigateButton button)
        {
            NavigatePanel.Children.Remove(button);
            if (MainWindowContent.Content == button.CurrentControl.Content)
            {
                MainWindowContent.Visibility = Visibility.Hidden;
            }
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

        private void ButtonHome_OnClickEolymp(object sender, RoutedEventArgs e)
        {
            OpenView(_tab);
        }
        private void ButtonCleanCache_OnClick(object sender, RoutedEventArgs e)
        {
            DataProvider.ClearCache();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        private void ButtonGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            DataProvider.Generate();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}