using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    public partial class ProblemPackWindow : Window
    {
        private readonly IViewNavigateService _navigateService;

        public readonly UserGroup _group;
        public readonly string _packTitle;
        public ProblemPackWindow(string packTitle, UserGroup group, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            _group = group;
            _packTitle = packTitle;
            InitializeComponent();
            Panel.Children.Add(new ProblemTaskPreview(this, "A"));
        }

        private void ButtonAddPack(object sender, RoutedEventArgs e)
        {
            var problems = new List<Problem>(); 
            IEnumerable<ProblemTaskPreview> taskList = Panel
                .Children
                .OfType<ProblemTaskPreview>()
                .Where(text => text.TaskNumberInput.Text != "");

            foreach (var task in taskList)
            {
                switch (task.DomainBox.Text)
                {
                    case "Eolymp":
                        problems.Add(new Problem(task.TaskNumberInput.Text, Model.Problems.Domain.EOlymp));
                        break;

                    case "Codeforces":
                        problems.Add(new Problem(task.TaskNumberInput.Text, Model.Problems.Domain.Codeforces));
                        break;
                }

            }

            //var problems = Problem.CreateEOlympFromList(taskList);
            
            _group.ProblemsPacks.Add(new ProblemsPack(_packTitle, problems));
            //TODO:
            DataProvider.ProblemsPackRepository.Create(_group.Title, new ProblemsPack(_packTitle, problems));

            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
