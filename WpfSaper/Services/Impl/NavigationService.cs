using System;
using System.Windows;
using WpfSaper.Models;
using WpfSaper.Views;

namespace WpfSaper.Services.Impl
{
    public class NavigationService : INavigationService
    {
        public void ShowAboutBoxDialog(Window parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            AboutWindow aboutWindow = new AboutWindow() { Owner = parent };
            aboutWindow.Show();
        }

        public GameConfig ShowGameConfigDialog(Window parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            var window = new GameConfigWindow() { Owner = parent };
            bool? dialogResult = window.ShowDialog();
            if (dialogResult.GetValueOrDefault())
            {
                return window.GameConfig;
            }
            return null;
        }
    }
}
