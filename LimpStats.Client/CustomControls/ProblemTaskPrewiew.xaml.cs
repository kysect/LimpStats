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

namespace LimpStats.Client.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для ProblemTaskPrewiew.xaml
    /// </summary>
    public partial class ProblemTaskPrewiew : UserControl
    {
        private ProblemPackWindow _problemPackWindow;
        public ProblemTaskPrewiew(ProblemPackWindow problemPackWindow)
        {
            InitializeComponent();
            _problemPackWindow = problemPackWindow;
        }
        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _problemPackWindow.Panel.Children.Add(new ProblemTaskPrewiew(_problemPackWindow));
            }
        }
    }
}
