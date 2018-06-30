using System.Windows;

namespace WpfSaper.Services.Impl
{
    class DialogService : IDialogService
    {
        public MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(message, title, button, icon);
        }
    }
}
