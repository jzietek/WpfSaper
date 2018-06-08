using System.Windows;

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
            MessageBox.Show("This tile is not part of the game :)", "Hey!", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
