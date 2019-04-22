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
            string EolympLogin = LoginEolympTextBox.Text;
            string CodeforcesHandle = LoginCodeforcesTextBox.Text;
            string nickname = NameBox.Text;
            if (!Parser.IsUserExist(EolympLogin) && _users.Count(f => f.EOlympLogin == EolympLogin) != 0)
            {
                MessageBox.Show("Неверный логин Eolymp");
                return;
            }
            //TODO: проверка существования codeforces-аккаунта
            if (Parser.IsUserExist(EolympLogin) && _users.Count(f => f.CodeforcesHandle == CodeforcesHandle) == 0)
            {
                //TODO: какая-то галочка проверки возле текстбокса
            }
            else
            {
                MessageBox.Show("Неверный логин Codeforces или пользователь с таким хэндлом уже существует");
                return;
            }
            if(_users.Count(f => f.Username == nickname) == 0)
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
            UsernameEolymp = EolympLogin;
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
