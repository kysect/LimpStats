using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LimpStats.Client.CustomControls.Blocks;
using LimpStats.Client.Models;
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
        private readonly StudentPackBlockPreview _blockPreview;
        private readonly UserGroup _group;
        private readonly string _packTitle;
        private readonly string _groupTitle;
        private readonly Domain _domain;
        public ProblemPackWindow(string packTitle, UserGroup group, StudentPackBlockPreview blockPreview, string groupTitle, IViewNavigateService navigateService, Domain domain)
        {
            _navigateService = navigateService;
            _domain = domain;
            _blockPreview = blockPreview;
            _group = group;
            _packTitle = packTitle;
            //TODO: мб все груптайтлы вынести в StudentGroup ибо она везде используется
            //TODO: все еще не пофиксил
            _groupTitle = groupTitle;
            InitializeComponent();
            Panel.Children.Add(new ProblemTaskPreview(this, "A"));
        }

        private void ButtonAddPack(object sender, RoutedEventArgs e)
        {
            List<int> taskList = Panel
                .Children
                .OfType<ProblemTaskPreview>()
                .Where(text => text.TaskNumberInput.Text != "")
                .Select(task => int.Parse(task.TaskNumberInput.Text))
                .ToList();
            

            _group.ProblemsPacks.Add(new ProblemsPack(_packTitle, Problem.CreateEOlympFromList(taskList)));
            JsonBackupManager.SaveCardUserList(_group, _groupTitle);

            _blockPreview.PackListPanel.Children.Add(new ProblemTasksPreview(_blockPreview, _group, _packTitle, _navigateService));
            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
