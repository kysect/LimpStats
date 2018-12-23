using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Database.Models;

namespace LimpStats.Client.CustomControls
{
    public partial class StudentGroupBlock : UserControl
    {
        public readonly SumVar SumVar;
        private readonly Grid _stackPanel;
        private readonly StackPanel _NavigatePanel;

        public StudentGroupBlock(SumVar sumVar, Grid stackPanel, StackPanel NavigatePanel)
        {
            InitializeComponent();
            _NavigatePanel = NavigatePanel;
            SumVar = sumVar;
            _stackPanel = stackPanel;
        }

        public void AddGroupToPanel(object sender, RoutedEventArgs e)
        {
            var groupPanel = (StackPanel)FindName("Panel");
            groupPanel.Children.Add(new StudentGroupPreview(this, FilePath.Text, _stackPanel, _NavigatePanel));

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
