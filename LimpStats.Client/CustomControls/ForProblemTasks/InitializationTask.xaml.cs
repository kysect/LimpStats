using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.Tools;
using LimpStats.Core.Parsers;

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
            _problemPackWindow.PanelViewer.ScrollToEnd();
            textbox.Focus();
        }

  
        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IsEnabled = false;
                string num = Core.Tools.Tools.GenerateNextNumber(NumberTask.Content.ToString());
                _problemPackWindow.Panel.Children.Add(new ProblemTaskPreview(_problemPackWindow, num));
                try
                {
                    var n = int.Parse(textbox.Text);
                    TaskName.Content = Parser.GetTitleTask(n);
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"{exception}");
                }

            }
        }

   
    }
}
