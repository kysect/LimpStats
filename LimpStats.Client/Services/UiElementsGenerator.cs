using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LimpStats.Model;

namespace LimpStats.Client.Services
{
    public static class UiElementsGenerator
    {
        public static Button CreateButton(int id, double marginLeft)
        {
            return new Button
            {
                Name = "Refresh",
                Width = 200,
                Height = 50,
                DataContext = $"grid{id}",
                Content = "Refresh",
                Margin = new Thickness(marginLeft + 250, 100, 0, 0)
            };
        }
        public static Button CreateUserButton(string id, Thickness margin, string login, int CompletedTaskCount)
        {
            return new Button
            {
                Name = login,
                Width = 185,
                Height = 50,
                DataContext = id+login,
                Content = $"{login, 15}    {CompletedTaskCount}",
                Background = new SolidColorBrush(Color.FromRgb(124, 124, 124)),
                //      Margin = new Thickness(margin.Left-350, margin.Top-150, 0, 0)
            };
        }
        public static ListBox CreateDataGrid(int id, double marginLeft)
        {
            return new ListBox
            {
                Name = $"grid{id}",
                Margin = new Thickness(marginLeft + 250, 150, 0, 0),
                //FrozenColumnCount = 1,
                //MinColumnWidth = 50,
                //GridLinesVisibility = DataGridGridLinesVisibility.None,
                Background = new SolidColorBrush(Color.FromRgb(49, 49, 49)),
                //RowBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
        }
    }
}