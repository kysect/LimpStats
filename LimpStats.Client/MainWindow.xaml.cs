using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls;
using LimpStats.Client.Tools;

namespace LimpStats.Client
{
    public partial class MainWindow : Window, IViewNavigateService
    {
        public MainWindow()
        {
            InitializeComponent();
            var f = new StudentGroupBlock(this);
            AddToViewList("Main", f);
            OpenView(f);
        }

        public void RemoveButton(NavigateButton button)
        {
            NavigatePanel.Children.Remove(button);
            //TODO: implement closing content window
        }

        public void AddToViewList(string viewTitle, UserControl view)
        {
            NavigatePanel.Children.Add(new NavigateButton(viewTitle, this, view));
        }

        public void OpenView(UserControl view)
        {
            MainWindowContent.Content = view.Content;
        }
    }
}