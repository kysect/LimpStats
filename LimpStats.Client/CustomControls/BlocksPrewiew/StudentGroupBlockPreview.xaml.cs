using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Client.Tools;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.BlocksPrewiew
{
    public partial class StudentGroupBlockPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;
        public StudentGroupBlockPreview(IViewNavigateService navigateService)
        {
            _navigateService = navigateService;
            InitializeComponent();
            UpdateUi();
        }
        public void UpdateUi()
        {
            ThreadingTools.ExecuteUiThread(() => GroupListPanel.Children.Clear());
            List<UserGroup> groups = DataProvider.UserGroupRepository.ReadAll();
            foreach (UserGroup group in groups)
            {
                var preview = new StudentGroupPreview(group.Title, _navigateService);
                ThreadingTools.ExecuteUiThread(() => GroupListPanel.Children.Add(preview));
            }
        }
        public void AddGroupToPanel(object sender, RoutedEventArgs e)
        {
            //TODO:
            UserGroup group = DataProvider.UserGroupRepository.Read(FilePath.Text);
            //var cards = JsonBackupManager.LoadCardName();
            if (group != null)
            {
                MessageBox.Show($"The name of group must be unique!");
            }
            else
            {
                GroupListPanel.Children.Add(new StudentGroupPreview(FilePath.Text, _navigateService));

                FilePath.Text = string.Empty;
            }
        }

        private void FilePath_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= FilePath_OnGotFocus;
        }

        private void FilePath_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            AddList.IsEnabled = tb.Text != string.Empty;
        }

        private void FilePath_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddGroupToPanel(null, null);
            }
        }
    }
}
