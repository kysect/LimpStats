using System;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class StudentSettingsControl : UserControl
    {
        private UserGroup _group;
        private LimpUser _user;
        private Action _updateUI;
        public StudentSettingsControl(LimpUser user, UserGroup group, Action updateUi)
        {
            InitializeComponent();
            _updateUI = updateUi;
            _group = group;
            _user = user;
            Title.Content = user.Username;
        }

        private void Update_User(object sender, RoutedEventArgs e)
        {
            
        }

        private void DelUser(object sender, RoutedEventArgs e)
        {
            _group.Users.Remove(_user);
            DataProvider.UserGroupRepository.Update(_group);
            _updateUI();
        }
    }
}
