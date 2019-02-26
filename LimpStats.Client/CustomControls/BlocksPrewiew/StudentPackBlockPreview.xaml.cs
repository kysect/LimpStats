using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.CustomControls.ForProblemTasks;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;
using Domain = LimpStats.Client.Models.Domain;

namespace LimpStats.Client.CustomControls.BlocksPrewiew
{
    public partial class StudentPackBlockPreview : UserControl
    {
        private readonly UserGroup _userGroup;
        private readonly IViewNavigateService _navigateService;
        private readonly Domain _domain;
        public StudentPackBlockPreview(UserGroup userGroup, IViewNavigateService navigateService, Domain domain)
        {
            _navigateService = navigateService;
            _domain = domain;
            InitializeComponent();
            _userGroup = userGroup;

            foreach (ProblemsPack pack in userGroup.ProblemsPacks)
            {
                var taskPreview = new ProblemTasksPreview(_userGroup, pack.Title, _navigateService, _domain);
                PackListPanel.Children.Add(taskPreview);
                PanelViewer.ScrollToRightEnd();
            }

        }

        public void OnClick_UpdatePanel(object sender, RoutedEventArgs e)
        {
            if (_userGroup.ProblemsPacks.Any(p => p.Title == PackTitleInput.Text))
                {
                    MessageBox.Show($"The name of group must be unique!");
                    return;
                }
            var packWindow = new ProblemPackWindow(PackTitleInput.Text, _userGroup, _navigateService, _domain);
            packWindow.Show();
           PackListPanel.Children.Add(new ProblemTasksPreview(packWindow._group, packWindow._packTitle, _navigateService, _domain));
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
