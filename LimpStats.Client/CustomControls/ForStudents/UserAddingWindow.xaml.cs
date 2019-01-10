using System;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Core.Parsers;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class UserAddingWindow : Window
    {
        public string Username;

        public UserAddingWindow()
        {
            InitializeComponent();
        }
        
        private void ValidateLogin(object sender, EventArgs e)
        {
            string username = LoginTextBox.Text;
            if (Parser.IsUserExist(username))
            {
                MessageBox.Show($"{username} добавлен");
                Username = username;
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин");
            }
        }

        private void LoginTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var loginBox = (TextBox)sender;
            loginBox.Text = string.Empty;
            loginBox.GotFocus -= LoginTextBox_OnGotFocus;
        }
    }
}
