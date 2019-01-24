using System.Linq;
using System.Windows;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    public partial class ProblemPackWindow : Window
    {
        private readonly IViewNavigateService _navigateService;

        //TODO: fix typo
        private readonly StudentPackBlockPrewiew _blockPrewiew;
        private readonly UserGroup _group;
        private readonly string _packTitle;
        private readonly string _groupTitle;

        public ProblemPackWindow(string packTitle, UserGroup group, StudentPackBlockPrewiew blockPrewiew, string groupTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            _blockPrewiew = blockPrewiew;
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
            

            _group.ProblemsPacks.Add(new ProblemsPack(_packTitle, EOlympProblem.CreateFromList(taskList)));
            JsonBackupManager.SaveCardUserList(_group, _groupTitle);

            _blockPrewiew.PackListPanel.Children.Add(new ProblemTasksPreview(_blockPrewiew, _group, _packTitle, _navigateService));
            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
