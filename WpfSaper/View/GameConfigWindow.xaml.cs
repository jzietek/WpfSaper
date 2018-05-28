﻿using System;
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
using WpfSaper.Services;
using WpfSaper.ViewModel;

namespace WpfSaper
{
    /// <summary>
    /// Interaction logic for DifficultySelectionWindow.xaml
    /// </summary>
    public partial class DifficultySelectionWindow : Window
    {
        public GameConfigViewModel ViewModel { get; set; }

        public DifficultySelectionWindow()
        {
            InitializeComponent();            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}