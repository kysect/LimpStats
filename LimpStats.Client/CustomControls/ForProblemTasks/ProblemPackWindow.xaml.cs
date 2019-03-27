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
        public bool IsUpdPack = false;

        public ProblemPackWindow(string packTitle, UserGroup group, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            Group = group;
            PackTitle = packTitle;
            InitializeComponent();
            Panel.Children.Add(new ProblemTaskPreview(this, "A"));
        }
        public ProblemPackWindow(ProblemsPack pack, UserGroup group)
        {
         
            InitializeComponent();
            Group = group;
            PackTitle = pack.Title;
            IsUpdPack = true;
            var num = "";
            foreach (var problem in pack.Problems)
            {
                Panel.Children.Add(new ProblemTaskPreview(this, problem, num = Core.Tools.Tools.GenerateNextNumber(num)));
            }
            Panel.Children.Add(new ProblemTaskPreview(this, Core.Tools.Tools.GenerateNextNumber(num)));

        }
        private void ButtonAddPack(object sender, RoutedEventArgs e)
        {
            if (IsUpdPack)
            {
                DataProvider.ProblemsPackRepository.Update(Group.Title, CreatePack());
                PanelViewer.ScrollToRightEnd();
                Close();
            }
            else
            {
                var pack = CreatePack();
                Group.ProblemsPacks.Add(pack);
                DataProvider.ProblemsPackRepository.Create(Group.Title, new ProblemsPack(PackTitle, pack.Problems));

                PanelViewer.ScrollToRightEnd();
                Close();
            }
        }

        public ProblemsPack CreatePack()
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
            return new ProblemsPack(PackTitle, problems);
            //var problems = Problem.CreateEOlympFromList(taskList);

        }
    }
}
