using Gat.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfSaper.Commands;
using WpfSaper.Model;
using WpfSaper.Services;
using WpfSaper.Services.Impl;

namespace WpfSaper.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand newGameCommand;
        private ICommand showAboutBoxCommand;
        private ICommand exitApplicationCommand;
        private ICommand tileClickedCommand;
        private ICommand tileRightClickedCommand;

        private Minefield minefield;

        private readonly IMinefieldFactory minefieldFactory;

        public MainWindowViewModel()
        {
            this.minefieldFactory = new MinefieldFactory(new RandomBooleansGenerator());
            this.Minefield = this.minefieldFactory.CreateNew(12, 12, 10); //TODO not to be harcoded            
        }

        private void Minefield_GameEnded(object sender, Minefield.GameEndedEventArgs e)
        {
            if (e.IsWon)
            {
                MessageBox.Show("Minefield cleared. You won!", ":)");
            }
            else
            {
                MessageBox.Show("Game Over!", ":(" );
            }
        }

        public Minefield Minefield
        {
            get
            {
                return minefield;
            }
            set
            {
                if (minefield != value)
                {
                    if (minefield != null)
                    {
                        minefield.GameEnded -= Minefield_GameEnded;
                    }

                    minefield = value;
                    minefield.GameEnded += Minefield_GameEnded;

                    OnPropertyChanged("Minefield");
                }
            }
        }        

        public ICommand ShowAboutBoxCommand
        {
            get
            {
                if (showAboutBoxCommand == null)
                {
                    showAboutBoxCommand = new RelayCommand( (arg) => { ShowAboutBoxWindow(); } );
                }
                return showAboutBoxCommand;
            }
        }

        public ICommand NewGameCommand
        {
            get
            {
                if (newGameCommand == null)
                {
                    newGameCommand = new RelayCommand((arg) => { StartNewGame(); });
                }
                return newGameCommand;
            }
        }

        public ICommand ExitApplicationCommand
        {
            get
            {
                if (exitApplicationCommand == null)
                {
                    exitApplicationCommand = new RelayCommand((arg) => { Application.Current.Shutdown(); });
                }
                return exitApplicationCommand;
            }
        }

        public ICommand TileClickedCommand
        {
            get
            {
                if (tileClickedCommand == null)
                {
                    tileClickedCommand = new RelayCommand((arg) => { HandleTileClicked(arg); });
                }
                return tileClickedCommand;
            }
        }

        public ICommand TileRightClickedCommand
        {
            get
            {
                if (tileRightClickedCommand == null)
                {
                    tileRightClickedCommand = new RelayCommand((arg) => { HandleTileRightClicked(arg); });
                }
                return tileRightClickedCommand;
            }
        }       
        

        private void HandleTileLeftAndRightClicked(object arg)
        {
            Tile tile = arg as Tile;
            if (tile != null)
            {
                foreach(var n in tile.Neighbours)
                {
                    n.UncoverTile();
                }
            }            
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void HandleTileRightClicked(object arg)
        {
            if (AreBothNouseKeysPressed())
            {
                HandleTileLeftAndRightClicked(arg);
            }

            if (!this.minefield.IsGameEnded)
            {
                (arg as Tile)?.ToggleFlag();
            }
        }

        private bool AreBothNouseKeysPressed()
        {
            return (Mouse.LeftButton == MouseButtonState.Pressed && Mouse.RightButton == MouseButtonState.Pressed);
        }

        private void HandleTileClicked(object arg)
        {
            if (AreBothNouseKeysPressed())
            {
                HandleTileLeftAndRightClicked(arg);
            }

            if (!this.minefield.IsGameEnded)
            {
                (arg as Tile)?.UncoverTile();
            }
        }

        private void StartNewGame()
        {
            this.Minefield = this.minefieldFactory.CreateNew(3, 3, 1);
        }

        private void ShowAboutBoxWindow()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }
    }
}
