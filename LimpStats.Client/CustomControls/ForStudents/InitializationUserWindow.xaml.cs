using System;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls
{
    //TODO: Стоит переименовать
    public partial class InitializationCardWindow : Window
    {
        private readonly StudyGroup _group;
        private string _groupTitle;
        public InitializationCardWindow(StudyGroup group, string grouptitle)
        {
            InitializeComponent();
            _group = group;
            _groupTitle = grouptitle;
        }
        
        private void ValidateLogin(object sender, EventArgs e)
        {
            string username = LoginTextBox.Text;
            if (Parser.IsUserExist(username))
            {
                MessageBox.Show($"{username} добавлен");
                _group.UserList.Add(new LimpUser(username));
                JsonBackupManager.SaveCardUserList(_group, _groupTitle);
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
