using System.Collections.Generic;
using System.Windows.Controls;
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.BlocksSettings
{
    public partial class StudentGroupBlockSettings : UserControl
    {
        public StudentGroupBlockSettings()
        {
            InitializeComponent();
            UpdateUi();
        }

        public void UpdateUi()
        {
            ThreadingTools.ExecuteUiThread(() => GroupListPanel.Children.Clear());
            List<UserGroup> groups = DataProvider.UserGroupRepository.ReadAll();
            foreach (UserGroup group in groups)
            {
                var settings = new StudentGroupSettings(group, UpdateUi);
                ThreadingTools.ExecuteUiThread(() => GroupListPanel.Children.Add(settings));
            }
        }
    }
}
