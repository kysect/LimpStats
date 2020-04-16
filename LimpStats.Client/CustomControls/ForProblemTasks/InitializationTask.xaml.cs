using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Core.CodeforcesParser;
using LimpStats.Core.Parsers;
using LimpStats.Model;
using LimpStats.Model.Problems;

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
        public ProblemTaskPreview(ProblemPackWindow problemPackWindow, Problem problem, string number)
        {
            InitializeComponent();

            NumberTask.Content = number;

            TaskNumberInput.Text = problem.Title;
            TaskNumberInput.IsEnabled = false;

            DomainBox.Text = problem.Type.ToUiString();
            DomainBox.IsEnabled = false;
            
            _problemPackWindow = problemPackWindow;
            _problemPackWindow.PanelViewer.ScrollToEnd();
        }



        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BlockProblem(sender, e);
            }
        }

        private void BlockProblem(object sender, RoutedEventArgs e)
        {
            TaskNumberInput.IsEnabled = false;
            DomainBox.IsEnabled = false;
            string num = Core.Tools.Tools.GenerateNextNumber(NumberTask.Content.ToString());
            _problemPackWindow.Panel.Children.Add(new ProblemTaskPreview(_problemPackWindow, num));
            try
            {
                Domain domain = DomainExtensions.Parse(DomainBox.Text);
                switch (domain)
                {
                    case Domain.EOlymp:
                        int n = int.Parse(TaskNumberInput.Text);
                        TaskName.Content = Parser.GetTitleTask(n);
                        break;

                    case Domain.Codeforces:
                        int contestId = Int32.Parse(TaskNumberInput.Text.Remove(TaskNumberInput.Text.Length - 1));
                        string letter = TaskNumberInput.Text[TaskNumberInput.Text.Length - 1].ToString();
                        TaskName.Content = CodeforcesProfileParser.GetTitleName(contestId, letter);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(domain.ToString());
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception}");
            }
        }
    }
}
