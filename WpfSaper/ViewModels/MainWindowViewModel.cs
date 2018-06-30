using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WpfSaper.Commands;
using WpfSaper.Models;
using WpfSaper.Services;
using WpfSaper.Services.Impl;
using WpfSaper.Views;
using Loc = WpfSaper.Localization.Resources;

namespace WpfSaper.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private ICommand newGameCommand;
        private ICommand restartGameCommand;
        private ICommand showAboutBoxCommand;
        private ICommand exitApplicationCommand;
        private ICommand tileClickedCommand;
        private ICommand tileRightClickedCommand;
        private ICommand tileLeftAndRightClickedCommand;
        private ICommand dummyTileClickedCommand;

        private Minefield minefield;
        private GameConfig gameConfig;
        private readonly IDialogService dialogService;        
        private readonly IMinefieldFactory minefieldFactory;
        private readonly INavigationService navigationService;

        public MainWindowViewModel(IMinefieldFactory minefieldFactory, 
            IDialogService dialogService, INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.minefieldFactory = minefieldFactory;
            this.dialogService = dialogService;
                        
            gameConfig = new GameConfig();
            gameConfig.SetMedium();
            Minefield = minefieldFactory.CreateNew(gameConfig);            
        }

        public MainWindowViewModel() : this(
            new MinefieldFactory(new RandomBooleansGenerator()), 
            new DialogService(), 
            new NavigationService())
        {
        }

        private void Minefield_GameEnded(object sender, Minefield.GameEndedEventArgs e)
        {
            MessageBoxResult result;
            if (e.IsWon)
            {
                result = dialogService.Show(Loc.MainWindow_Message_GameWon_Text, Loc.MainWindow_Message_GameWon_Title, MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            }
            else
            {
                result = dialogService.Show(Loc.MainWindow_Message_GameLost_Text, Loc.MainWindow_Message_GameLost_Title, MessageBoxButton.YesNo, MessageBoxImage.Hand);
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
            get { return minefield; }
            set
            {
                var oldMinefield = minefield;
                if (Set(ref minefield, value))
                {
                    if (minefield != null) minefield.GameEnded += Minefield_GameEnded;
                    if (oldMinefield != null) oldMinefield.GameEnded -= Minefield_GameEnded;
                }
            }
        }

        public ICommand DummyTileClickedCommand
        {
            get
            {
                return dummyTileClickedCommand ?? (dummyTileClickedCommand = new RelayCommand(_ => { ShowHeyMessage(); }));
            }
        }       

        public ICommand ShowAboutBoxCommand
        {
            get
            {
                return showAboutBoxCommand ?? (showAboutBoxCommand = new RelayCommand((arg) => { ShowAboutBoxWindow(arg as Window); }));
            }
        }

        public ICommand NewGameCommand
        {
            get
            {
                return newGameCommand ?? (newGameCommand = new RelayCommand((arg) => { ConfigureAndStartNewGame(arg as Window); }));
            }
        }

        public ICommand RestartGameCommand
        {
            get
            {
                return restartGameCommand ?? (restartGameCommand = new RelayCommand((arg) => { StartGame(); }));
            }
        }

        public ICommand ExitApplicationCommand
        {
            get
            {
                return exitApplicationCommand ?? (exitApplicationCommand = new RelayCommand((arg) => { System.Windows.Application.Current.Shutdown(); }));
            }
        }

        public ICommand TileClickedCommand
        {
            get
            {
                return tileClickedCommand ?? (tileClickedCommand = new RelayCommand((arg) => { HandleTileClicked(arg); }));
            }
        }

        public ICommand TileRightClickedCommand
        {
            get
            {
                return tileRightClickedCommand ?? (tileRightClickedCommand = new RelayCommand((arg) => { HandleTileRightClicked(arg); }));
            }
        }

        public ICommand TileLeftAndRightClickedCommand
        {
            get
            {
                return tileLeftAndRightClickedCommand ?? (tileLeftAndRightClickedCommand = new RelayCommand((arg) => { HandleTileLeftAndRightClicked(arg); }));
            }
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
            System.Diagnostics.Debug.WriteLine("Left clicked");
            if (!minefield.IsGameEnded)
            {
                (arg as Tile)?.UncoverTile();
            }
        }

        private void ConfigureAndStartNewGame(Window mainWindow)
        {            
            GameConfig newGameConfig = navigationService.ShowGameConfigDialog(mainWindow);
            if (newGameConfig != null)
            {
                gameConfig = newGameConfig;
                StartGame();
            }
        }

        private void StartGame()
        {
            Minefield = minefieldFactory.CreateNew(gameConfig);
        }

        private void ShowAboutBoxWindow(Window mainWindow)
        {
            navigationService.ShowAboutBoxDialog(mainWindow);
        }

        private void ShowHeyMessage()
        {
            dialogService.Show(Loc.MainWindow_HeyMessage, Loc.MainWindow_HeyMessage_Title, MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
