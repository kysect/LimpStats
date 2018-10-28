using System;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LimpStats.Client.CustomControls;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StackPanel.Children.Add(new StudentGroupBlock());
        }
    }
}