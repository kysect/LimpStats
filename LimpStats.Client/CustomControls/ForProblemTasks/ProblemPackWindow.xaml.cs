using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls;
using LimpStats.Client.CustomControls.ForProblemTasks;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client
{
    public partial class ProblemPackWindow : Window
    {
        private IViewNavigateService _navigateService;

        //TODO: clean this
        public List<int> tasklist = new List<int>();
        private string _name;
        private StudyGroup _group;
        private StudentPackBlock _block;
        private string _groupTitle;

        public ProblemPackWindow(string packname, StudyGroup group, StudentPackBlock block, string groupTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            _block = block;
            _group = group;
            _name = packname;
            //TODO: мб все груптайтлы вынести в StudentGroup ибо она везде используется
            _groupTitle = groupTitle;
            InitializeComponent();
            Panel.Children.Add(new ProblemTaskPreview(this, "A"));
        }

        private void ButtonAddPack(object sender, RoutedEventArgs e)
        {
            foreach (var task in Panel.Children.OfType<ProblemTaskPreview>())
            {
                tasklist.Add(Int32.Parse(task.textbox.Text == "" ? "0" : task.textbox.Text));
            }
            _group.ProblemPackList.Add(new ProblemPackInfo(_name, tasklist));
            JsonBackupManager.SaveCardUserList(_group, _groupTitle);
            var k = (StackPanel)_block.FindName("Panel");
            k.Children.Add(new ProblemTasksPrewiew(_block, _group, _name, _navigateService));
            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
