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
        public ProblemPackWindow()
        {
            InitializeComponent();
            Panel.Children.Add(new ProblemTaskPrewiew(this));
        }   
    }
}
