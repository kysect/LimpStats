using System.Linq;
using System.Windows;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.ForProblemTasks
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
            var taskList = Panel
                .Children
                .OfType<ProblemTaskPreview>()
                .Where(text => text.TaskNumberInput.Text != "")
                .Select(task => int.Parse(task.TaskNumberInput.Text))
                .ToList();
            

            _group.ProblemPackList.Add(new ProblemPackInfo(_packTitle, taskList));
            JsonBackupManager.SaveCardUserList(_group, _groupTitle);

            _block.PackListPanel.Children.Add(new ProblemTasksPreview(_block, _group, _packTitle, _navigateService));
            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
