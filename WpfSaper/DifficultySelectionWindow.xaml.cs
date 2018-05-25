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

namespace WpfSaper
{
    /// <summary>
    /// Interaction logic for DifficultySelectionWindow.xaml
    /// </summary>
    public partial class DifficultySelectionWindow : Window
    {
        public DifficultySelectionWindow()
        {
            InitializeComponent();
            this.GameConfig = new GameConfig();

        }

        public GameConfig GameConfig { get; private set; }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (optionEasy.IsChecked.Value)
                SetEasyConfig();
            else if (optionMedium.IsChecked.Value)
                SetMediumConfig();
            else if (optionHard.IsChecked.Value)
                SetHardConfig();

            this.DialogResult = true;
            this.Close();
        }

        private void SetEasyConfig()
        {
            this.GameConfig.Setup(6,6,8);
        }

        private void SetMediumConfig()
        {
            this.GameConfig.Setup(10, 10, 30);
        }

        private void SetHardConfig()
        {
            this.GameConfig.Setup(16, 16, 80);
        }
    }
}
