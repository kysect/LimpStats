using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    public partial class StudentPackBlock : UserControl
    {
        private readonly StudyGroup _users;

        public StudentPackBlock(StudyGroup users)
        {
            InitializeComponent();
            _users = users;
            foreach (var pack in users.ProblemPackList)
            {
                var k = (StackPanel)FindName("Panel");
                k.Children.Add(new StudentGroupPreview(this, _users, pack.PackTitle));
                PanelViewer.ScrollToRightEnd();
            }
        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            var f = new ProblemPackWindow(FilePath.Text, _users, this);
            f.Show();
        }

        private void TextBox1_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox1_OnGotFocus;
        }

        private void FilePath_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            AddPack.IsEnabled = tb.Text != string.Empty;
        }

        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnClick_UpdatePanel(new object(), new RoutedEventArgs());
            }
        }
    }
}
