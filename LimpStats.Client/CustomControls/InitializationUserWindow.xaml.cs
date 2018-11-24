using System;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Core.Parsers;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    //TODO: Стоит переименовать
    public partial class InitializationCardWindow : Window
    {
        private readonly StudyGroup _group;
        public InitializationCardWindow(StudyGroup group)
        {
            InitializeComponent();
            _group = group;
        }
        
        private void ValidateLogin(object sender, EventArgs e)
        {
            string username = LoginTextBox.Text;
            if (Parser.IsUserExist(username))
            {
                MessageBox.Show($"{username} добавлен");
                _group.UserList.Add(new LimpUser(username));
            }
            else
            {
                MessageBox.Show("Неверный логин");
            }

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
