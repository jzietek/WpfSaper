using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfSaper.Commands;
using WpfSaper.Model;
using WpfSaper.Services;
using WpfSaper.Services.Impl;
using WpfSaper.View;

namespace WpfSaper.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand newGameCommand;
        private ICommand restartGameCommand;
        private ICommand showAboutBoxCommand;
        private ICommand exitApplicationCommand;
        private ICommand tileClickedCommand;
        private ICommand tileRightClickedCommand;
        private ICommand tileLeftAndRightClickedCommand;

        private Minefield minefield;
        private GameConfig gameConfig;
        private readonly IMinefieldFactory minefieldFactory;

        public MainWindowViewModel()
        {
            minefieldFactory = new MinefieldFactory(new RandomBooleansGenerator());
            gameConfig = new GameConfig();
            gameConfig.SetMedium();
            Minefield = minefieldFactory.CreateNew(gameConfig);
        }

        private void Minefield_GameEnded(object sender, Minefield.GameEndedEventArgs e)
        {
            MessageBoxResult result = MessageBoxResult.None;
            if (e.IsWon)
            {
                result = MessageBox.Show("Minefield cleared. You won! One more time?", "You have won :)", MessageBoxButton.YesNo);
            }
            else
            {
                result = MessageBox.Show("Game Over! One more time?", "You have lost :(", MessageBoxButton.YesNo );
            }

            if (result == MessageBoxResult.No)
            {
                ExitApplicationCommand.Execute(null);
            }
            else if (result == MessageBoxResult.Yes)
            {
                RestartGameCommand.Execute(null);
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

                    OnPropertyChanged();
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
                    newGameCommand = new RelayCommand((arg) => { ConfigureAndStartNewGame(); });
                }
                return newGameCommand;
            }
        }

        public ICommand RestartGameCommand
        {
            get
            {
                if (restartGameCommand == null)
                {
                    restartGameCommand = new RelayCommand((arg) => { StartGame(); });
                }
                return restartGameCommand;
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

        public ICommand TileLeftAndRightClickedCommand
        {
            get
            {
                if (tileLeftAndRightClickedCommand == null)
                {
                    tileLeftAndRightClickedCommand = new RelayCommand((arg) => { HandleTileLeftAndRightClicked(arg); });
                }
                return tileLeftAndRightClickedCommand;
            }
        }        

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void HandleTileLeftAndRightClicked(object arg)
        {
            if (!minefield.IsGameEnded)
            {
                Tile tile = arg as Tile;
                if (tile != null)
                {
                    foreach (var n in tile.Neighbours)
                    {
                        if (n.State == Tile.TileState.Covered)
                        {
                            n.UncoverTile();
                        }
                    }
                }
            }
        }

        private void HandleTileRightClicked(object arg)
        {
            if (!minefield.IsGameEnded)
            {
                (arg as Tile)?.ToggleFlag();
            }
        }

        private void HandleTileClicked(object arg)
        {
            if (!minefield.IsGameEnded)
            {
                (arg as Tile)?.UncoverTile();
            }
        }

        private void ConfigureAndStartNewGame()
        {
            var gameConfigWindow = new GameConfigWindow();
            if(gameConfigWindow.ShowDialog().GetValueOrDefault())
            {
                this.gameConfig = gameConfigWindow.GameConfig;
                StartGame();
            }            
        }

        private void StartGame()
        {
            Minefield = minefieldFactory.CreateNew(gameConfig);
        }

        private void ShowAboutBoxWindow()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }
    }
}
