using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.Tools;

namespace LimpStats.Client.CustomControls
{
    public partial class NavigateButton : UserControl
    {
        private readonly IViewNavigateService _navigateService;
        private UserControl _currentControl;

        public NavigateButton(string viewName, IViewNavigateService navigateService, UserControl block)
        {
            InitializeComponent();

            _navigateService = navigateService;
            _currentControl = block;

            OpenViewButton.DataContext = viewName;
        }

        private void ShowUserControl(object sender, RoutedEventArgs e)
        {
            _navigateService.OpenView(_currentControl);
        }

        private void RemoveFromListButton(object sender, RoutedEventArgs e)
        {
            _navigateService.RemoveButton(this);
        }
    }
}
