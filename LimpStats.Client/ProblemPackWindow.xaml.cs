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
        public ProblemPackWindow(string packname, StudyGroup group, StudentPackBlock block)
        {
            _block = block;
            _group = group;
            _name = packname;
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
            //TODO: сохранить групу пак не сохраняется
            var k = (StackPanel)_block.FindName("Panel");
            k.Children.Add(new StudentGroupPreview(_block, _group, _name));
            PanelViewer.ScrollToRightEnd();
            Close();
        }
    }
}
