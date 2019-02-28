using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Core.CodeforcesParser;
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
                    switch (DomainBox.Text)
                    {
                        case "Eolymp":
                        {
                            int n = int.Parse(TaskNumberInput.Text);
                            TaskName.Content = Parser.GetTitleTask(n);
                        }
                            break;
                        case "Codeforces":
                        {
                            TaskName.Content = CodeforcesProfileParser.GetTitleName(Int32.Parse(TaskNumberInput.Text.Remove(TaskNumberInput.Text.Length - 1)), TaskNumberInput.Text[TaskNumberInput.Text.Length-1].ToString());
                        }
                            break;
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"{exception}");
                }

            }
        }
        

   
    }
}
