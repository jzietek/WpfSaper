using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSaper.services;
using WpfSaper.Services.Impl;
using Shouldly;

namespace WpfSaper.Tests.Services
{
    [TestFixture]
    public class RandomBooleansGeneratorTests
    {
        [Test]
        public void GenerateBooleans_ShouldThrowException_GivenInvalidInputs()
        {
            IBooleansGenerator generator = new RandomBooleansGenerator();
            Should.Throw<ArgumentException>(() => generator.GenerateBooleans(-1, -1));

            Should.Throw<ArgumentException>(() => generator.GenerateBooleans(-1, 0));

            Should.Throw<ArgumentException>(() => generator.GenerateBooleans(0, -1));

            Should.Throw<ArgumentException>(() => generator.GenerateBooleans(2, 3));
        }

        [Test]
        public void GenerateBooleans_ShouldReturnOnlyPositives_GivenSameInputs()
        {
            IBooleansGenerator generator = new RandomBooleansGenerator();
            bool[] result = generator.GenerateBooleans(10, 10);
            result.All(x => x == true).ShouldBeTrue();
        }

        [Test]
        public void GenerateBooleans_ShouldReturnOnlyNegatives_GivenZeroPositives()
        {
            IBooleansGenerator generator = new RandomBooleansGenerator();
            bool[] result = generator.GenerateBooleans(10, 0);
            result.All(x => x == false).ShouldBeTrue();
        }

        [Test]
        public void GenerateBooleans_ShouldReturnX_GivenXPositives()
        {
            IBooleansGenerator generator = new RandomBooleansGenerator();
            bool[] result = generator.GenerateBooleans(10, 7);
            result.Count(x => x == true).ShouldBe(7);
        }
    }
}
