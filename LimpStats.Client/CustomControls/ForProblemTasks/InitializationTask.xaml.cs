using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Core.Parsers;

namespace LimpStats.Client.CustomControls.ForProblemTasks
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
            TaskNumberInput.Focus();
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
                    int n = int.Parse(TaskNumberInput.Text);
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
