using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.CustomControls.ForProblemTasks;
using LimpStats.Client.Tools;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.Blocks
{
    public partial class StudentPackBlock : UserControl
    {
        private readonly StudyGroup _users;
        private readonly string _groupTitle;
        private readonly IViewNavigateService _navigateService;

        public StudentPackBlock(StudyGroup users, string groupTitle, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;

            InitializeComponent();
            _users = users;
            _groupTitle = groupTitle;

            foreach (ProblemPackInfo pack in users.ProblemPackList)
            {
                var taskPreview = new ProblemTasksPreview(this, _users, pack.PackTitle, _navigateService);
                PackListPanel.Children.Add(taskPreview);
                PanelViewer.ScrollToRightEnd();
            }

        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            var packWindow = new ProblemPackWindow(PackTitleInput.Text, _users, this, _groupTitle, _navigateService);
            packWindow.Show();
        }

        private void PackTitleInput_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= PackTitleInput_OnGotFocus;
        }

        private void PackTitleInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            AddPack.IsEnabled = string.IsNullOrWhiteSpace(tb.Text) == false;
        }

        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnClick_UpdatePanel(new object(), new RoutedEventArgs());
            }
        }
    }
}
