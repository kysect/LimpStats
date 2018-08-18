using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public static ListBox CreateDataGrid(int id, double marginLeft)
        {
            return new ListBox
            {
                Name = $"grid{id}",
                Margin = new Thickness(marginLeft + 250, 150, 0, 0),
                //FrozenColumnCount = 1,
                //MinColumnWidth = 50,
                //GridLinesVisibility = DataGridGridLinesVisibility.None,
                Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                //RowBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
        }
    }
}