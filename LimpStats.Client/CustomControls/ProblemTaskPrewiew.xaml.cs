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
using HtmlAgilityPack;

namespace LimpStats.Client.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для ProblemTaskPrewiew.xaml
    /// </summary>
    public partial class ProblemTaskPrewiew : UserControl
    {
        private ProblemPackWindow _problemPackWindow;
        public ProblemTaskPrewiew(ProblemPackWindow problemPackWindow, string number)
        {
            InitializeComponent();
            NumberTask.Content = number;
            _problemPackWindow = problemPackWindow;
            textbox.TabIndex = 0;
        }
        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IsEnabled = false;
                var num = GenerateNextNumber(NumberTask.Content.ToString());
                _problemPackWindow.Panel.Children.Add(new ProblemTaskPrewiew(_problemPackWindow, num));
                var tasknum =  this.textbox.Text;
                GetTitleTask(Int32.Parse(tasknum));
            }
        }

        private string GenerateNextNumber(string number)
        {
            //Todo: работает, но если есть варик попроще надо его юзать
            var n = number.ToCharArray();
            n[number.Length - 1]++;
            string s = "";
            if (n[number.Length - 1] > 'Z')
            {
                s = "A";
                n[number.Length - 1] = 'A';
            }
            foreach (var i in n)
            {
                s += i.ToString();
            }
            return s;
        }

        private void GetTitleTask(int number)
        {
            //TODO загрузка названия задачи
            string url = $"https://www.e-olymp.com/ru/problems/{number}";
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//*[contains(@class,'eo-title__header')]"))
                TaskName.Content = (node.ChildNodes[0].InnerHtml);
            
        }
    }
}
