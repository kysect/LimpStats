using System.Windows;
using System.Windows.Controls;
using LimpStats.Client.CustomControls;

namespace LimpStats.Client.Tools
{
    public interface IViewNavigateService
    {
        void RemoveButton(NavigateButton button);
        void AddToViewList(string viewTitle, UserControl view);
        void OpenView(UserControl view);
    }
}