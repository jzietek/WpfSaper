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
    }
}
