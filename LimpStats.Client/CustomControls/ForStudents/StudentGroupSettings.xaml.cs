using System;
using System.Windows;
using System.Windows.Controls;
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
            CardTitle.DataContext = _group.Title;
            foreach (LimpUser user in _group.Users)
            {
                Panel.Children.Add(new StudentSettingsControl(user, _group));
            }

        }
        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonClick_Del(object sender, RoutedEventArgs e)
        {
            DataProvider.UserGroupRepository.Delete(_group);
            _updateParentView();
        }
    }
}
