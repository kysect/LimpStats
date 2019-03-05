using System.Windows.Controls;
using LimpStats.Client.CustomControls.ForProblemTasks;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.BlocksSettings
{
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
