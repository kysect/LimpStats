using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Core.Parsers;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class UserAddingWindow : Window
    {
        public string Nickname;
        public string UsernameEolymp;
        public string UsernameCodeforces;
        private readonly List<LimpUser> _users;

        public UserAddingWindow(List<LimpUser> users)
        {
            InitializeComponent();
            _users = users;
        }
        
        private void ValidateLogin(object sender, EventArgs e)
        {
            string username = LoginEolympTextBox.Text;
            //TODO: проверка существования codeforces-аккаунта
            if (Parser.IsUserExist(username) && _users.Count(f => f.Username == username) == 0)
            {
                Nickname = NameBox.Text;
                MessageBox.Show($"{Nickname} добавлен");
                UsernameEolymp = username;
                UsernameCodeforces = LoginCodeforcesTextBox.Text;       
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пользователь уже добавлен в группу");
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
