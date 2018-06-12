using System.Windows;
using Loc = WpfSaper.Localization.Resources;

namespace WpfSaper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TileControl_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Loc.MainWindow_HeyMessage, Loc.MainWindow_HeyMessage_Title, MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
