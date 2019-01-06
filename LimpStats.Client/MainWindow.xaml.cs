using System.Windows;
using LimpStats.Client.CustomControls;
using LimpStats.Database.Models;

namespace LimpStats.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var f = new StudentGroupBlock(SumVar.AllPack, StackPanel, NavigatePanel);
            StackPanel.Children.Add(f);
            NavigatePanel.Children.Add(new NavigateButton(f, StackPanel, "Main", NavigatePanel));
        }
    }
}