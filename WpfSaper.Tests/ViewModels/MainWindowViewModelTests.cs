using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfSaper.Models;
using WpfSaper.Services;
using WpfSaper.Services.Impl;
using WpfSaper.ViewModels;

namespace WpfSaper.Tests.ViewModels
{
    [TestFixture]
    class MainWindowViewModelTests
    {
        [Test]
        public void DummyCommand_ShouldShowMessage()
        {
            //Arrange
            var minefieldFactory = new MinefieldFactory(new RandomBooleansGenerator());
            var dialogService = Substitute.For<IDialogService>();
            var navigationService = Substitute.For<INavigationService>();
            MainWindowViewModel vm = new MainWindowViewModel(minefieldFactory, dialogService, navigationService);

            //Act
            vm.DummyTileClickedCommand.Execute(null);

            //Assert
            dialogService.ReceivedWithAnyArgs(1).Show(null, null, 0, 0);
        }

        [Test]
        public void RestartGameCommand_ShouldSetNewGame()
        {
            //Arrange
            var minefieldFactory = new MinefieldFactory(new RandomBooleansGenerator());
            var dialogService = Substitute.For<IDialogService>();
            var navigationService = Substitute.For<INavigationService>();
            dialogService.Show(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MessageBoxButton>(), Arg.Any<MessageBoxImage>());

            MainWindowViewModel vm = new MainWindowViewModel(minefieldFactory, dialogService, navigationService);
            int gameHashCode = vm.Minefield.GetHashCode();

            //Act
            vm.RestartGameCommand.Execute(null);

            //Assert
            int newGameHashCode = vm.Minefield.GetHashCode();
            newGameHashCode.ShouldNotBe(gameHashCode);
        }

        [Test]
        public void NewGameCommand_ShouldSetShowNewGameDialog()
        {
            //Arrange
            var minefieldFactory = new MinefieldFactory(new RandomBooleansGenerator());
            var dialogService = Substitute.For<IDialogService>();
            var navigationService = Substitute.For<INavigationService>();
            dialogService.Show(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MessageBoxButton>(), Arg.Any<MessageBoxImage>());

            MainWindowViewModel vm = new MainWindowViewModel(minefieldFactory, dialogService, navigationService);

            //Act
            vm.NewGameCommand.Execute(null);

            //Assert
            navigationService.Received(1).ShowGameConfigDialog(Arg.Any<Window>());
        }

        [Test]
        public void TileRightClickedCommand_ShouldMarkOneFlag_AndThenUnmark()
        {
            //Arrange
            var minefieldFactory = new MinefieldFactory(new RandomBooleansGenerator());
            var dialogService = Substitute.For<IDialogService>();
            var navigationService = Substitute.For<INavigationService>();
            dialogService.Show(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MessageBoxButton>(), Arg.Any<MessageBoxImage>());

            MainWindowViewModel vm = new MainWindowViewModel(minefieldFactory, dialogService, navigationService);
            Tile firstTile = vm.Minefield.Tiles.First().First();
            vm.Minefield.TilesFlagged.ShouldBe(0);
            firstTile.State.ShouldNotBe(Tile.TileState.Flagged);
            firstTile.State.ShouldBe(Tile.TileState.Covered);

            //Act
            vm.TileRightClickedCommand.Execute(firstTile);

            //Assert
            vm.Minefield.TilesFlagged.ShouldBe(1);
            firstTile.State.ShouldBe(Tile.TileState.Flagged);
            firstTile.State.ShouldNotBe(Tile.TileState.Covered);

            //Act
            vm.TileRightClickedCommand.Execute(firstTile);

            //Assert
            vm.Minefield.TilesFlagged.ShouldBe(0);
            firstTile.State.ShouldNotBe(Tile.TileState.Flagged);
            firstTile.State.ShouldBe(Tile.TileState.Covered);
        }
    }
}
