using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfSaper.Commands;
using WpfSaper.Models;
using WpfSaper.Services;
using WpfSaper.Services.Impl;
using WpfSaper.Views;
using Loc = WpfSaper.Localization.Resources;

namespace WpfSaper.ViewModels
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
            MessageBoxResult result;
            if (e.IsWon)
            {
                result = MessageBox.Show(Loc.MainWindow_Message_GameWon_Text, Loc.MainWindow_Message_GameWon_Title, MessageBoxButton.YesNo);
            }
            else
            {
                result = MessageBox.Show(Loc.MainWindow_Message_GameLost_Text, Loc.MainWindow_Message_GameLost_Title, MessageBoxButton.YesNo);
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
                    showAboutBoxCommand = new RelayCommand((arg) => { ShowAboutBoxWindow(arg as Window); });
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
                    newGameCommand = new RelayCommand((arg) => { ConfigureAndStartNewGame(arg as Window); });
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
            System.Diagnostics.Debug.WriteLine("Left and right clicked");

            if (!minefield.IsGameEnded && arg is Tile tile)
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

        private void HandleTileRightClicked(object arg)
        {
            System.Diagnostics.Debug.WriteLine("Right clicked");
            if (!minefield.IsGameEnded)
            {
                (arg as Tile)?.ToggleFlag();
            }
        }

        private void HandleTileClicked(object arg)
        {
            System.Diagnostics.Debug.WriteLine("Left  clicked");
            if (!minefield.IsGameEnded)
            {
                (arg as Tile)?.UncoverTile();
            }
        }

        private void ConfigureAndStartNewGame(Window mainWindow)
        {
            var gameConfigWindow = new GameConfigWindow() { Owner = mainWindow };

            if (gameConfigWindow.ShowDialog().GetValueOrDefault())
            {
                gameConfig = gameConfigWindow.GameConfig;
                StartGame();
            }
        }

        private void StartGame()
        {
            Minefield = minefieldFactory.CreateNew(gameConfig);
        }

        private static void ShowAboutBoxWindow(Window mainWindow)
        {
            AboutWindow aboutWindow = new AboutWindow() { Owner = mainWindow };
            aboutWindow.Show();
        }
    }
}
