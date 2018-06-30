using NSubstitute;
using NUnit.Framework;
using Shouldly;
using WpfSaper.Models;
using WpfSaper.Services;
using WpfSaper.Services.Impl;

namespace WpfSaper.Tests.Models
{
    [TestFixture]
    public class MinefieldTests
    {
        [Test]
        public void ToggleFlag_ShouldToggle()
        {
            IBooleansGenerator booleansGenerator = new RandomBooleansGenerator();
            IMinefieldFactory factory = new MinefieldFactory(booleansGenerator);
            var minefield = factory.CreateNew(5, 5, 10);

            var firstTile = minefield.Tiles[0][0];
            firstTile.State.ShouldBe(Tile.TileState.Covered);
            firstTile.ToggleFlag();
            firstTile.State.ShouldBe(Tile.TileState.Flagged);
            firstTile.ToggleFlag();
            firstTile.State.ShouldBe(Tile.TileState.Covered);
        }

        [Test]
        public void OneMineMinefield_PropagationShouldUncoverAllButOne()
        {
            //Arrange
            IBooleansGenerator booleansGenerator = Substitute.For<IBooleansGenerator>();
            booleansGenerator.GenerateBooleans(Arg.Any<int>(), Arg.Any<int>())
                 .Returns(x =>
                 {
                     var r = new bool[(int)x[0]];
                     r[0] = true; //Only the first item will be set to true.
                    return r;
                 });

            int horizontalTilesCount = 5;
            int verticalTilesCount = 5;
            IMinefieldFactory factory = new MinefieldFactory(booleansGenerator);
            var minefield = factory.CreateNew(horizontalTilesCount, verticalTilesCount, 1);
            var someTile = minefield.Tiles[3][3];

            //Act
            minefield.TilesCovered.ShouldBe(horizontalTilesCount * verticalTilesCount);
            someTile.UncoverTile();

            //Assert
            minefield.TilesCovered.ShouldBe(1);
        }
    }
}
