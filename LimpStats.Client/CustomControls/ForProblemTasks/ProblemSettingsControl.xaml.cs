using System;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    public partial class ProblemSettingsControl : UserControl
    {
        private UserGroup _group;
        private ProblemsPack _pack;
        private Problem _problem;
        private Action _updateUI;
        public ProblemSettingsControl(UserGroup group, ProblemsPack pack, Problem problem, Action updateUI)
        {
            InitializeComponent();
            Title.Content = problem.Title;
            _group = group;
            _pack = pack;
            _problem = problem;
            _updateUI = updateUI;
        }

        private void Update_Problem(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DelProblem(object sender, RoutedEventArgs e)
        {
            DataProvider.ProblemsPackRepository.DeleteProblem(_group.Title, _pack, _problem);
            _updateUI();
        }
    }
}
