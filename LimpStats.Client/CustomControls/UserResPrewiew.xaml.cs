using System.Windows.Controls;

namespace LimpStats.Client.CustomControls
{
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
