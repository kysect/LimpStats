using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Client.Tools;

namespace LimpStats.Client.CustomControls
{
    public partial class StudentGroupBlock : UserControl
    {
        private IViewNavigateService _navigateService;

        public StudentGroupBlock(IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            InitializeComponent();
        }

        public void AddGroupToPanel(object sender, RoutedEventArgs e)
        {
            var groupPanel = (StackPanel)FindName("Panel");
            groupPanel.Children.Add(new StudentGroupPreview(this, FilePath.Text, _navigateService));

            FilePath.Text = string.Empty;
        }

        private void FilePath_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= FilePath_OnGotFocus;
        }

        private void FilePath_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            AddList.IsEnabled = tb.Text != string.Empty;
        }

        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddGroupToPanel(null, null);
            }
        }
    }
}
