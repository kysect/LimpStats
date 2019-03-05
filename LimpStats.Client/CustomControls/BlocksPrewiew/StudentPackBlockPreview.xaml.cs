using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.CustomControls.ForProblemTasks;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.BlocksPrewiew
{
    public partial class StudentPackBlockPreview : UserControl
    {
        private readonly UserGroup _userGroup;
        private readonly IViewNavigateService _navigateService;

        public StudentPackBlockPreview(UserGroup userGroup, IViewNavigateService navigateService)
        {
            _navigateService = navigateService;
            InitializeComponent();
            _userGroup = userGroup;

            foreach (ProblemsPack pack in userGroup.ProblemsPacks)
            {
                var taskPreview = new ProblemTasksPreview(_userGroup, pack.Title, _navigateService);
                PackListPanel.Children.Add(taskPreview);
                PanelViewer.ScrollToRightEnd();
            }

        }
        public void UpdateUi()
        {
            ThreadingTools.ExecuteUiThread(() => PackListPanel.Children.Clear());
            var packs = DataProvider.UserGroupRepository.Read(_userGroup.Title).ProblemsPacks;
            foreach (var pack in packs)
            {
                var settings = new ProblemTasksPreview(_userGroup, pack.Title, _navigateService);
                ThreadingTools.ExecuteUiThread(() => PackListPanel.Children.Add(settings));
            }
        }
        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            if (_userGroup.ProblemsPacks.Any(p => p.Title == PackTitleInput.Text))
                {
                    MessageBox.Show($"The name of group must be unique!");
                    return;
                }
            var packWindow = new ProblemPackWindow(PackTitleInput.Text, _userGroup, _navigateService);
            packWindow.Show();
           PackListPanel.Children.Add(new ProblemTasksPreview(packWindow.Group, packWindow.PackTitle, _navigateService));
        }

        //TODO: возможно, стоит вынести это в отдельный тулзовый класс т.к. это логика будет использовать в нескольких классах
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
