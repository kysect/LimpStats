using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.Tools;

namespace LimpStats.Client
{
    public partial class MainWindow : Window, IViewNavigateService
    {
        public MainWindow()
        {
            InitializeComponent();
            var studentGroupBlock = new StudentGroupBlock(this);
            AddToViewList("Main", studentGroupBlock);
            OpenView(studentGroupBlock);
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