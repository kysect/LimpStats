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

namespace LimpStats.Client
{
    /// <summary>
    /// Логика взаимодействия для InitializationCardWinow.xaml
    /// </summary>
    public partial class InitializationCardWinow : Window
    {
        private MainWindow.MyDelegate d;
        public InitializationCardWinow(MainWindow.MyDelegate sender)
        {
            InitializeComponent();
            d = sender;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(LimpStats.Core.Parser.LoginValidation(textBox1.Text))
                d(textBox1.Text);
            else
            {
                MessageBox.Show("Неверный логин");
                
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           // TextBox textBox = (TextBox)sender;
           // MessageBox.Show(textBox.Text);
        }
    }
}
