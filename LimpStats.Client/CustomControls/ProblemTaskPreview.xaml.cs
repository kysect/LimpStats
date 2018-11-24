using System;
using System.Windows.Controls;
using System.Windows.Input;
using HtmlAgilityPack;

namespace LimpStats.Client.CustomControls
{
    public partial class ProblemTaskPreview : UserControl
    {
        private readonly ProblemPackWindow _problemPackWindow;

        public ProblemTaskPreview(ProblemPackWindow problemPackWindow, string number)
        {
            InitializeComponent();

            NumberTask.Content = number;
            _problemPackWindow = problemPackWindow;

            //TODO: wtf?
            textbox.TabIndex = 0;
        }
        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IsEnabled = false;
                string num = GenerateNextNumber(NumberTask.Content.ToString());
                _problemPackWindow.Panel.Children.Add(new ProblemTaskPreview(_problemPackWindow, num));
                GetTitleTask(number: Int32.Parse(textbox.Text));
            }
        }

        private string GenerateNextNumber(string number)
        {
            //Todo: работает, но если есть варик попроще надо его юзать
            //TODO: Вынести в .Core? создать там папку /Tools

            var n = number.ToCharArray();
            n[number.Length - 1]++;
            string s = "";
            if (n[number.Length - 1] > 'Z')
            {
                s = "A";
                n[number.Length - 1] = 'A';
            }

            foreach (char i in n)
            {
                s += i.ToString();
            }
            return s;
        }

        //TODO: Прееместить в .Core.Parsers
        private void GetTitleTask(int number)
        {
            //TODO загрузка названия задачи
            string url = $"https://www.e-olymp.com/ru/problems/{number}";
            var Webget = new HtmlWeb();
            HtmlDocument doc = Webget.Load(url);

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//*[contains(@class,'eo-title__header')]"))
                TaskName.Content = (node.ChildNodes[0].InnerHtml);
        }
    }
}
