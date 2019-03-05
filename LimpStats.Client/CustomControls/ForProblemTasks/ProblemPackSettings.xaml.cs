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
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
    /// <summary>
    /// Логика взаимодействия для ProblemPackSettings.xaml
    /// </summary>
    public partial class ProblemPackSettings : UserControl
    {
        private ProblemsPack _pack;
        private Action _updateParentView;
        private UserGroup _group;
        public ProblemPackSettings(UserGroup group, ProblemsPack pack , Action updateParentView)
        {
            InitializeComponent();
            _pack = pack;
            _updateParentView = updateParentView;
            _group = group;
            Update();
        }
        private void Update()
        {
            ThreadingTools.ExecuteUiThread(() => Panel.Children.Clear());
            CardTitle.DataContext = _pack.Title;
            foreach (var problem in _pack.Problems)
            {
                ThreadingTools.ExecuteUiThread(() => Panel.Children.Add(new ProblemSettingsControl(_group, _pack, problem, _updateParentView)));
            }

        }
        private void AddProblemsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonClick_DelPack(object sender, RoutedEventArgs e)
        {
            DataProvider.ProblemsPackRepository.Delete(_group.Title, _pack);
            _updateParentView();
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
