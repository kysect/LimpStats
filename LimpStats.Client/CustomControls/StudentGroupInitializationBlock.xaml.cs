using System.Windows;
using System.Windows.Controls;

namespace LimpStats.Client.CustomControls
{
    /// <summary>
    ///     Логика взаимодействия для StudentGroupInitializationBlock.xaml
    /// </summary>
    public partial class StudentGroupInitializationBlock : UserControl
    {
        public StudentGroupInitializationBlock()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var a = new MainWindow();
            //TODO: wtf??o_O
            a.OnClick_UpdatePanel(sender, e);
        }
    }
}