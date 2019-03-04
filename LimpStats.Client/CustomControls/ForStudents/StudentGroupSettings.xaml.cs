using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForStudents
{
    /// <summary>
    /// Логика взаимодействия для StudentGroupSettings.xaml
    /// </summary>
    public partial class StudentGroupSettings : UserControl
    {
        private UserGroup _group;
        private Action _updateParentView;

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
            foreach (var user in _group.Users)
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
