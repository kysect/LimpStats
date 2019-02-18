using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.CustomControls.Tabs;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Database;
using StudentGroupBlockPreview = LimpStats.Client.CustomControls.BlocksPrewiew.StudentGroupBlockPreview;

namespace LimpStats.Client
{
    public partial class MainWindow : Window, IViewNavigateService
    {
        private readonly StudentGroupTab _tabEolymp;
        private readonly StudentGroupTab _tabCodeforces;
        public MainWindow()
        {
            InitializeComponent();
            var studentGroupBlockEolymp = new StudentGroupBlockPreview(this, Domain.Eolymp);
            var studentGroupTabEolymp = new StudentGroupTab(studentGroupBlockEolymp);
            OpenView(studentGroupTabEolymp);
            _tabEolymp = studentGroupTabEolymp;

            var studentGroupBlockCodeforces = new StudentGroupBlockPreview(this, Domain.Codeforces);
            var studentGroupTabCodeforces = new StudentGroupTab(studentGroupBlockCodeforces);
            OpenView(studentGroupTabCodeforces);
            _tabCodeforces = studentGroupTabCodeforces;

        }

        public void RemoveButton(NavigateButton button)
        {
            NavigatePanel.Children.Remove(button);
            if (MainWindowContent.Content == button.CurrentControl.Content)
            {
                MainWindowContent.Visibility = Visibility.Hidden;
            }
        }

        public void AddToViewList(string viewTitle, UserControl view, Domain domain)
        {
            NavigatePanel.Children.Add(new NavigateButton(viewTitle, this, view, domain));
        }

        public void OpenView(UserControl view)
        {
            MainWindowContent.Visibility = Visibility.Visible;
            MainWindowContent.Content = view.Content;
        }

        private void ButtonHome_OnClickEolymp(object sender, RoutedEventArgs e)
        {
            OpenView(_tabEolymp);
        }
        private void ButtonHome_OnClickCodeforces(object sender, RoutedEventArgs e)
        {
            OpenView(_tabCodeforces);
        }

        private void ButtonCleanCache_OnClick(object sender, RoutedEventArgs e)
        {
            JsonBackupManager.ClearCache();
        }
    }
}