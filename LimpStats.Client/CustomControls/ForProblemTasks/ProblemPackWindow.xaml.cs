using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;
using StudentPackBlockPreview = LimpStats.Client.CustomControls.BlocksPrewiew.StudentPackBlockPreview;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    public partial class ProblemPackWindow : Window
    {
        private readonly IViewNavigateService _navigateService;

        public readonly UserGroup _group;
        public readonly string _packTitle;
        public readonly Domain _domain;
        public ProblemPackWindow(string packTitle, UserGroup group, IViewNavigateService navigateService, Domain domain)
        {
            _navigateService = navigateService;
            _domain = domain;

            _group = group;
            _packTitle = packTitle;
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

            var problempack = Problem.CreateEOlympFromList(taskList);
            _group.ProblemsPacks.Add(new ProblemsPack(_packTitle, problempack));
            //TODO:
            DataProvider.ProblemsPackRepository.Create(_group.Title, new ProblemsPack(_packTitle, problempack));
            //JsonBackupManager.SaveCardUserList(_group, _group.Title);


            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
