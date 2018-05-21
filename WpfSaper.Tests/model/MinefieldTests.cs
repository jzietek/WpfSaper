using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSaper.Services;
using WpfSaper.Services.Impl;

namespace WpfSaper.Tests
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
            firstTile.State.ShouldBe(Model.Tile.TileState.Covered);
            firstTile.ToggleFlag();
            firstTile.State.ShouldBe(Model.Tile.TileState.Flagged);
            firstTile.ToggleFlag();
            firstTile.State.ShouldBe(Model.Tile.TileState.Covered);
        }
    }
}
