using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        private readonly GameConfigViewModel gameConfigViewModel;

        public MainWindowViewModel()
        {
            minefieldFactory = new MinefieldFactory(new RandomBooleansGenerator());
            gameConfigViewModel = new GameConfigViewModel();

            Minefield = minefieldFactory.CreateNew(gameConfigViewModel.GameConfig);
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
                NewGameCommand.Execute(null);
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
                    if (n.State == Tile.TileState.Covered)
                    {
                        n.UncoverTile();
                    }
                }
            }            
        }

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void HandleTileRightClicked(object arg)
        {
            if (AreBothNouseKeysPressed())
            {
                HandleTileLeftAndRightClicked(arg);
            }

            if (!minefield.IsGameEnded)
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

            if (!minefield.IsGameEnded)
            {
                (arg as Tile)?.UncoverTile();
            }
        }

        private void ConfigureAndStartNewGame()
        {
            var gameConfigWindow = new DifficultySelectionWindow();
            gameConfigWindow.ViewModel = this.gameConfigViewModel;

            if(gameConfigWindow.ShowDialog().GetValueOrDefault())
            {
                Minefield = minefieldFactory.CreateNew(gameConfigWindow.ViewModel.GameConfig);
            }
        }

        private void ShowAboutBoxWindow()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }
    }
}
