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
    /// Логика взаимодействия для UserResPrewiew.xaml
    /// </summary>
    public partial class UserResPrewiew : UserControl
    {
        public UserResPrewiew(string username, int points)
        {
            InitializeComponent();
            Title.Content = username;
            Result.Content = points;
        }
    }
}
