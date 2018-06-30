using System.Windows;
using WpfSaper.Models;

namespace WpfSaper.Services
{
    interface INavigationService
    {
        GameConfig ShowGameConfigDialog(Window parent);

        void ShowAboutBoxDialog(Window parent);
    }
}
