using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.CustomControls.ForStudents;
using LimpStats.Client.Models;
using LimpStats.Client.Tools;
using LimpStats.Database;

namespace LimpStats.Client.CustomControls.BlocksPrewiew
{
    public partial class StudentGroupBlockPreview : UserControl
    {
        private readonly IViewNavigateService _navigateService;
        private readonly Domain _domain;
        public StudentGroupBlockPreview(IViewNavigateService navigateService, Domain domain)
        {
            _navigateService = navigateService;
            _domain = domain;

            InitializeComponent();
            
            //TODO:
            var groups = DataProvider.UserGroupRepository.ReadAll();
            //List<string> cards = JsonBackupManager.LoadCardName();
            foreach (var group in groups)
            {
                GroupListPanel.Children.Add(new StudentGroupPreview(group.Title, _navigateService, _domain));
            }

        }

        public void AddGroupToPanel(object sender, RoutedEventArgs e)
        {
            //TODO:
            var group = DataProvider.UserGroupRepository.Read(FilePath.Text);
            //var cards = JsonBackupManager.LoadCardName();
            if (group != null)
            {
                MessageBox.Show($"The name of group must be unique!");
            }
            else
            {
                GroupListPanel.Children.Add(new StudentGroupPreview(FilePath.Text, _navigateService, _domain));

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
