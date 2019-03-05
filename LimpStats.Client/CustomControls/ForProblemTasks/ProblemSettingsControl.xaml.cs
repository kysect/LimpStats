using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    /// <summary>
    /// Логика взаимодействия для ProblemSettingsControl.xaml
    /// </summary>
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
