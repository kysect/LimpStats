using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HtmlAgilityPack;

namespace LimpStats.Client
{
    /// <summary>
    /// Логика взаимодействия для InitializationCardWinow.xaml
    /// </summary>
    public partial class InitializationCardWinow : Window
    {
        private Action<string> d;

        private int s = -1;
        private string textboxcontent;
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
        private int LoginValidation()
        {
            var client = new HtmlWeb();
            var link = $"https://www.e-olymp.com/ru/users/{textboxcontent}";

            if (client.Load(link).Text.Contains($"{textboxcontent}"))
                return 1;
            return 0;
        }
        private async void TextBox_TextChanged(object sender, EventArgs e)
        {
            textboxcontent = textBox1.Text;
            int result = await Task.Run(() =>
            {
                var res = LoginValidation();
                return res == 1 ? 1 : 0;
            });
            if (result == 1)
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
    }
}
