using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfSaper.Model;
using WpfSaper.Services;
using WpfSaper.ViewModel;

namespace WpfSaper.View
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

        private void btnOk_Click(object sender, RoutedEventArgs e)
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
