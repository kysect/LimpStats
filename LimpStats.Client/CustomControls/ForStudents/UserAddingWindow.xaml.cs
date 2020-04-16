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
            string eolympLogin = LoginEolympTextBox.Text;
            string codeforcesHandle = LoginCodeforcesTextBox.Text;
            string nickname = NameBox.Text;

            var elimpParser = new ElimpParser();
            var cfParser = new CodeforcesProfileParser();
            

            if (!elimpParser.IsUserExist(eolympLogin)
                || _users.Any(f => f.EOlympLogin == eolympLogin))
            {
                MessageBox.Show("Неверный логин Eolymp или пользователь с таким хэндлом уже добавлен");
                return;
            }

            if (!cfParser.IsUserExist(codeforcesHandle)
                || _users.Any(f => f.CodeforcesHandle == codeforcesHandle))
            {
                MessageBox.Show("Неверный логин Codeforces или пользователь с таким хэндлом уже добавлен");
                return;
            }

            if(_users.All(f => f.Username != nickname))
            {
                //TODO: какая-то галочка проверки возле текстбокса
            }
            else
            {
                MessageBox.Show("Пользователь с таким ником уже существует");
                return;
            }

            Nickname = nickname;
            MessageBox.Show($"{Nickname} добавлен");
            UsernameEolymp = eolympLogin;
            UsernameCodeforces = LoginCodeforcesTextBox.Text;
            Close();
        }

        private void LoginTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var loginBox = (TextBox)sender;
            loginBox.Text = string.Empty;
            loginBox.GotFocus -= LoginTextBox_OnGotFocus;
        }
    }
}
