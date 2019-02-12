using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.Tools;
using ResultGridBlock = LimpStats.Client.CustomControls.BlocksPrewiew.ResultGridBlock;

namespace LimpStats.Client.CustomControls
{
    public partial class NavigateButton : UserControl
    {
        private readonly IViewNavigateService _navigateService;
        public readonly UserControl CurrentControl;

        public NavigateButton(string viewName, IViewNavigateService navigateService, UserControl block)
        {
         
            InitializeComponent();
            if (block is ResultGridBlock)
            {
                //TODO: а так точно должно быть? о_О
                icon.Source = new ImageSourceConverter().ConvertFromString("pack://application:,,,/LimpStats.Client;component/icons/iconTable-03.png") as ImageSource;      
            }
            _navigateService = navigateService;
            CurrentControl = block;

            OpenViewButton.DataContext = viewName;
        }

        private void ShowUserControl(object sender, RoutedEventArgs e)
        {
            _navigateService.OpenView(CurrentControl);
        }

        private void RemoveFromListButton(object sender, RoutedEventArgs e)
        {
            _navigateService.RemoveButton(this);
        }
    }
}
