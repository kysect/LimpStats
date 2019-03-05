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

        public readonly UserGroup Group;
        public readonly string PackTitle;

        public ProblemPackWindow(string packTitle, UserGroup group, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            Group = group;
            PackTitle = packTitle;
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
                        problems.Add(new Problem(task.TaskNumberInput.Text, Domain.EOlymp));
                        break;

                    case "Codeforces":
                        problems.Add(new Problem(task.TaskNumberInput.Text, Domain.Codeforces));
                        break;
                }

            }

            //var problems = Problem.CreateEOlympFromList(taskList);
            
            Group.ProblemsPacks.Add(new ProblemsPack(PackTitle, problems));
            DataProvider.ProblemsPackRepository.Create(Group.Title, new ProblemsPack(PackTitle, problems));

            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
