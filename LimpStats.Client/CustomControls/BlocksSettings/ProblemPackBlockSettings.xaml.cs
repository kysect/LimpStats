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
using LimpStats.Client.CustomControls.ForProblemTasks;
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.BlocksSettings
{
    /// <summary>
    /// Логика взаимодействия для ProblemPackBlockSettings.xaml
    /// </summary>
    public partial class ProblemPackBlockSettings : UserControl
    {
        private UserGroup _group;
        public ProblemPackBlockSettings(UserGroup group)
        {
            InitializeComponent();
            _group = group;
            UpdateUi();
        }
        public void UpdateUi()
        {
            ThreadingTools.ExecuteUiThread(() => PackListPanel.Children.Clear());
            var packs = DataProvider.UserGroupRepository.Read(_group.Title).ProblemsPacks;
            foreach (var pack in packs)
            {
                var settings = new ProblemPackSettings(_group, pack, UpdateUi);
                ThreadingTools.ExecuteUiThread(() => PackListPanel.Children.Add(settings));
            }
        }
    }
}
