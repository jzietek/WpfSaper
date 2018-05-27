using System.Globalization;
using System.Threading;
using System.Windows;

namespace WpfSaper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            base.OnStartup(e);
        }
    }
}
