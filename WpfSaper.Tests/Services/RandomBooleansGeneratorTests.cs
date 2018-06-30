using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSaper.Services;
using WpfSaper.Services.Impl;
using Shouldly;

namespace WpfSaper.Tests.Services
{
    [TestFixture]
    public class RandomBooleansGeneratorTests
    {
        [TestCase(-1,-1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(2, 3)]
        public void GenerateBooleans_ShouldThrowException_GivenInvalidInputs(int allCount, int positivesCount)
        {
            IBooleansGenerator generator = new RandomBooleansGenerator();
            Should.Throw<ArgumentException>(() => generator.GenerateBooleans(allCount, positivesCount));
        }

        [Test]
        public void GenerateBooleans_ShouldReturnOnlyPositives_GivenSameInputs()
        {
            IBooleansGenerator generator = new RandomBooleansGenerator();
            bool[] result = generator.GenerateBooleans(10, 10);
            result.All(x => x).ShouldBeTrue();
        }

        [Test]
        public void GenerateBooleans_ShouldReturnOnlyNegatives_GivenZeroPositives()
        {
            IBooleansGenerator generator = new RandomBooleansGenerator();
            bool[] result = generator.GenerateBooleans(10, 0);
            result.All(x => x).ShouldBeTrue();
        }

        [TestCase(10,7)]
        [TestCase(100, 99)]
        [TestCase(100, 1)]
        [TestCase(0, 0)]
        public void GenerateBooleans_ShouldReturnX_GivenXPositives(int allCount, int positivesCount)
        {
            IBooleansGenerator generator = new RandomBooleansGenerator();
            bool[] result = generator.GenerateBooleans(allCount, positivesCount);
            result.Length.ShouldBe(allCount);
            result.Count(x => x).ShouldBe(positivesCount);
            result.Count(x => !x).ShouldBe(allCount - positivesCount);
        }

        [TestCase(5,3,13)]
        public void GenerateBooleans_ShouldReturnSame_ForSameSeed(int allCount, int positivesCount, int seed)
        {
            IBooleansGenerator generatorA = new RandomBooleansGenerator(seed);
            IBooleansGenerator generatorB = new RandomBooleansGenerator(seed);
            bool[] resultA = generatorA.GenerateBooleans(allCount, positivesCount);
            bool[] resultB = generatorB.GenerateBooleans(allCount, positivesCount);
            resultA.SequenceEqual(resultB).ShouldBeTrue();
        }

        [TestCase(50, 33, 13, 14)]
        public void GenerateBooleans_ShouldReturnDifferent_ForDifferentSeed(int allCount, int positivesCount, int seedA, int seedB)
        {
            IBooleansGenerator generatorA = new RandomBooleansGenerator(seedA);
            IBooleansGenerator generatorB = new RandomBooleansGenerator(seedB);
            bool[] resultA = generatorA.GenerateBooleans(allCount, positivesCount);
            bool[] resultB = generatorB.GenerateBooleans(allCount, positivesCount);
            resultA.SequenceEqual(resultB).ShouldBeFalse();
        }
    }
}
