using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LimpStats.Client.CustomControls.BlocksPrewiew;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;
using Domain = LimpStats.Client.Models.Domain;
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
            List<Problem> problems = new List<Problem>(); 
             var taskList = Panel
                .Children
                .OfType<ProblemTaskPreview>()
                .Where(text => text.TaskNumberInput.Text != "");
            foreach (var task in taskList)
            {
                switch (task.DomainBox.Text)
                {
                    case "Eolymp":
                    {
                          problems.Add(new Problem(task.TaskNumberInput.Text, Model.Problems.Domain.EOlymp));
                    }
                        break;
                    case "Codeforces":
                    {
                        problems.Add(new Problem(task.TaskNumberInput.Text, Model.Problems.Domain.Codeforces));

                        }
                        break;
                }

            }
                //.Select(task => int.Parse(task.TaskNumberInput.Text))
                //.ToList();

            //var problems = Problem.CreateEOlympFromList(taskList);
            
            _group.ProblemsPacks.Add(new ProblemsPack(_packTitle, problems));
            //TODO:
            DataProvider.ProblemsPackRepository.Create(_group.Title, new ProblemsPack(_packTitle, problems));
            //JsonBackupManager.SaveCardUserList(_group, _group.Title);


            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
