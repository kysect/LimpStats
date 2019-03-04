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
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Database;

namespace LimpStats.Client.CustomControls.BlocksSettings
{
    /// <summary>
    /// Логика взаимодействия для StudentGroupBlockSettings.xaml
    /// </summary>
    public partial class StudentGroupBlockSettings : UserControl
    {
        public StudentGroupBlockSettings()
        {
            InitializeComponent();
            var groups = DataProvider.UserGroupRepository.ReadAll();
            foreach (var group in groups)
            {
                GroupListPanel.Children.Add(new StudentGroupSettings(group));
            }
        }
    }
}
