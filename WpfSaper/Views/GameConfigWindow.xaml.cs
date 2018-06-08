using System.Windows;
using WpfSaper.Models;
using WpfSaper.ViewModels;

namespace WpfSaper.Views
{
    /// <summary>
    /// Interaction logic for GameConfigWindow.xaml
    /// </summary>
    public partial class GameConfigWindow : Window
    {
        public GameConfigWindow()
        {            
            InitializeComponent();            
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public GameConfig GameConfig
        {
            get
            {
                return (this.DataContext as GameConfigViewModel)?.GameConfig;
            }
        }
    }
}
