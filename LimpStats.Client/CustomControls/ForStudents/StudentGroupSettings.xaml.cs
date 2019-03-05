using System;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForStudents
{
    public partial class StudentGroupSettings : UserControl
    {
        private readonly UserGroup _group;
        private readonly Action _updateParentView;

        public StudentGroupSettings(UserGroup group, Action updateParentView)
        {
            InitializeComponent();
            _group = group;
            _updateParentView = updateParentView;
            Update();
        }

        private void Update()
        {
            ThreadingTools.ExecuteUiThread(() => Panel.Children.Clear());
            CardTitle.DataContext = _group.Title;
            foreach (LimpUser user in _group.Users)
            {
                ThreadingTools.ExecuteUiThread(() => Panel.Children.Add(new StudentSettingsControl(user, _group, _updateParentView)));
            }

        }
        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO: изменение названия группы
        }

        private void ButtonClick_Del(object sender, RoutedEventArgs e)
        {
            DataProvider.UserGroupRepository.Delete(_group);
            _updateParentView();
        }

        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            var userAdding = new UserAddingWindow(_group.Users);
            userAdding.ShowDialog();
            //TODO: костыли
            if (userAdding.NameBox.Text != "Name")
            {
                _group.Users.Add(
                    new LimpUser(userAdding.UsernameEolymp, userAdding.UsernameCodeforces, userAdding.Name));
                DataProvider.UserGroupRepository.Update(_group);
                Update();
            }
            //JsonBackupManager.SaveCardUserList(_group, _studentGroupTitle);
        }
    }
}
