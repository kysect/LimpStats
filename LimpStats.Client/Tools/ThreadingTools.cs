using System;
using System.Windows;

namespace LimpStats.Client.Tools
{
    public static class ThreadingTools
    {
        public static void ExecuteUiThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}