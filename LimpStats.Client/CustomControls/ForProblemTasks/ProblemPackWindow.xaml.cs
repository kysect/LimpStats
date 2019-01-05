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
using System.Windows.Shapes;
using LimpStats.Client.CustomControls;
using LimpStats.Client.CustomControls.ForProblemTasks;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client
{
    /// <summary>
    /// Логика взаимодействия для ProblemPackWindow.xaml
    /// </summary>
    public partial class ProblemPackWindow : Window
    {
        public List<int> tasklist = new List<int>();
        private string _name;
        private StudyGroup _group;
        private StudentPackBlock _block;
        private Grid _panel;
        private string _groupTitle;
        private StackPanel _navigatepanel;
        public ProblemPackWindow(string packname, StudyGroup group, StudentPackBlock block, string groupTitle, Grid panel, StackPanel navigatepanel)
        {
            _navigatepanel = navigatepanel;
            _panel = panel;
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
            k.Children.Add(new ProblemTasksPrewiew(_block, _group, _name, _panel, _navigatepanel));
            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
