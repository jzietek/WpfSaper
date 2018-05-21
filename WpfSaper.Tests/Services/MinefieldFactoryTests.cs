using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSaper.Services;
using WpfSaper.Services.Impl;

namespace WpfSaper.Tests.Services
{
    [TestFixture]
    class MinefieldFactoryTests
    {
        [TestCase(5,5,25)]
        [TestCase(5, 5, 10)]
        [TestCase(5, 5, 0)]
        public void CreateNew_ShouldReturn_ValidMinefield(int rows, int columns, int bombsCount)
        {
            IBooleansGenerator booleansGenerator = new RandomBooleansGenerator();
            IMinefieldFactory factory = new MinefieldFactory(booleansGenerator);
            var result = factory.CreateNew(rows, columns, bombsCount);

            result.Tiles.SelectMany(t => t).Count().ShouldBe(rows * columns);
            result.BombsInMinefiled.ShouldBe(bombsCount);
            result.Tiles.SelectMany(t => t).All(t => t.Neighbours.Count() >= 3 && t.Neighbours.Count() <= 8).ShouldBeTrue();
        }

        [TestCase(5, 5, 26)]
        [TestCase(3, 3, 10)]
        [TestCase(0, 0, 1)]
        public void CreateNew_ShouldThrowArgumentException_WhenMoreMinesThanTiles(int rows, int columns, int bombsCount)
        {
            IBooleansGenerator booleansGenerator = new RandomBooleansGenerator();
            IMinefieldFactory factory = new MinefieldFactory(booleansGenerator);
            Should.Throw<ArgumentException>(() => factory.CreateNew(rows, columns, bombsCount));
        }
    }
}
