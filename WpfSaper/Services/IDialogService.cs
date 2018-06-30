using System.Windows;

namespace WpfSaper.Services
{
    interface IDialogService
    {
        MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage icon);
    }
}
