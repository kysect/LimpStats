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
    /// Логика взаимодействия для StudentSettingsControl.xaml
    /// </summary>
    public partial class StudentSettingsControl : UserControl
    {
        private UserGroup _group;
        private LimpUser _user;
        public StudentSettingsControl(LimpUser user, UserGroup group)
        {
            InitializeComponent();
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
        }
    }
}
