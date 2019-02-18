using System.Windows.Controls;
using LimpStats.Client.CustomControls;
using LimpStats.Client.Models;

namespace LimpStats.Client.Tools
{
    public interface IViewNavigateService
    {
        void RemoveButton(NavigateButton button);
        void AddToViewList(string viewTitle, UserControl view, Domain domain);
        void OpenView(UserControl view);
    }
}