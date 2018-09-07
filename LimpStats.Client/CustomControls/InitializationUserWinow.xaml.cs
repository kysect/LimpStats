using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HtmlAgilityPack;

namespace LimpStats.Client.CustomControls
{
    public partial class InitializationCardWinow : Window
    {
        private Action<string> d;

        private int s = -1;
        public InitializationCardWinow(Action<string> sender)
        {
            InitializeComponent();
            d = sender;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            if(LimpStats.Core.Parser.LoginValidation(textBox1.Text))
                d(textBox1.Text);
            else
            {
                MessageBox.Show("Неверный логин");
            }
        }

        private async void TextBox_TextChanged(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            //TODO: Каждый раз запускается таск, но если я сразу вверду два символа, то будет одновременно
            //два запроса + ты не знаешь, какой из низ быстрее выполниться: с одной буквой
            //или с двумя. Все же лучше сделать кнопку "Check"
            bool isExist = await Task.Run(() => Core.Parser.IsUserExist(username));
            if (isExist)
            {
                button.Visibility = Visibility.Visible;
                textBox2.Content = "OK";
            }
            else
            {
                button.Visibility = Visibility.Hidden;
                textBox2.Content = "X";
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
