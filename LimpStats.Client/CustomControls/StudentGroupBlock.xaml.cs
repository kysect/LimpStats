using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для StudentGroupBlock.xaml
    /// </summary>
    public partial class StudentGroupBlock : UserControl
    {
        public StudentGroupBlock()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            var f = new InitializationCardWindow(new StudyGroup());
            f.ShowDialog();
        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            var k = (StackPanel)FindName("Panel");
            k.Children.Add(new StudentGroupPreview(this, FilePath.Text));
            FilePath.Text = string.Empty;
            PanelViewer.ScrollToRightEnd();
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
            AddList.IsEnabled = tb.Text != string.Empty;
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
