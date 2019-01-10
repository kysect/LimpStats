using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LimpStats.Client.CustomControls;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.CustomControls.ForProblemTasks;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client
{
    public partial class ProblemPackWindow : Window
    {
        private readonly IViewNavigateService _navigateService;

        private readonly StudentPackBlock _block;
        private readonly StudyGroup _group;
        private readonly string _packTitle;
        private readonly string _groupTitle;

        public ProblemPackWindow(string packTitle, StudyGroup group, StudentPackBlock block, string groupTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            _block = block;
            _group = group;
            _packTitle = packTitle;
            //TODO: мб все груптайтлы вынести в StudentGroup ибо она везде используется
            _groupTitle = groupTitle;
            InitializeComponent();
            Panel.Children.Add(new ProblemTaskPreview(this, "A"));
        }

        private void ButtonAddPack(object sender, RoutedEventArgs e)
        {
            var taskList = new List<int>();

            foreach (ProblemTaskPreview task in Panel.Children.OfType<ProblemTaskPreview>())
            {
                taskList.Add(int.Parse(task.textbox.Text == "" ? "0" : task.textbox.Text));
            }

            _group.ProblemPackList.Add(new ProblemPackInfo(_packTitle, taskList));
            JsonBackupManager.SaveCardUserList(_group, _groupTitle);

            _block.PackListPanel.Children.Add(new ProblemTasksPreview(_block, _group, _packTitle, _navigateService));
            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
