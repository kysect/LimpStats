using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Core.Parsers;

namespace LimpStats.Client.CustomControls
{
    public partial class InitializationCardWindow : Window
    {
        public InitializationCardWindow()
        {
            InitializeComponent();
        }
        private void ValidateLogin(object sender, EventArgs e)
        {
           
            if(Parser.IsUserExist(LoginTextBox.Text))
            {
                MessageBox.Show($"{LoginTextBox.Text} !");
            }
            else
            {
                MessageBox.Show("Неверный логин");
            }
        }

        private async void TextBox_TextChanged(object sender, EventArgs e)
        {
            string username = LoginTextBox.Text;
            
            //TODO: Каждый раз запускается таск, но если я сразу вверду два символа, то будет одновременно
            //два запроса + ты не знаешь, какой из низ быстрее выполниться: с одной буквой
            //или с двумя. Все же лучше сделать кнопку "Check"
            bool isExist = await Task.Run(() => Parser.IsUserExist(username));
            if (isExist)
            {
                AddUserButton.Visibility = Visibility.Visible;
                StatusLabel.Content = "OK";
            }
            else
            {
                AddUserButton.Visibility = Visibility.Hidden;
                StatusLabel.Content = "X";
            }
        }

        private void TextBox1_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox1_OnGotFocus;
        }
    }
}
