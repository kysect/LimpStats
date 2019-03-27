using System;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.ForProblemTasks
{
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
            var packWindow = new ProblemPackWindow(_pack, _group);
            packWindow.Show();
          
            _updateParentView();

        }

        private void ButtonClick_DelPack(object sender, RoutedEventArgs e)
        {
            DataProvider.ProblemsPackRepository.Delete(_group.Title, _pack);
            _updateParentView();
        }

        private void CardTitle_OnClick(object sender, RoutedEventArgs e)
        {
           //TODO: редактирование имени пака
        }
    }
}
