using System;
using System.Collections.Generic;
using System.Linq;
using WpfSaper.Services;

namespace WpfSaper.Services.Impl
{
    class RandomBooleansGenerator : IBooleansGenerator
    {
        private readonly Random rand;

        public RandomBooleansGenerator()
        {
            rand = new Random(DateTime.Now.Millisecond);
        }

        public RandomBooleansGenerator(int seed)
        {
            rand = new Random(seed);
        }

        public bool[] GenerateBooleans(int allCount, int positivesCount)
        {
            if (positivesCount < 0)
            {
                throw new ArgumentException("Parameter positivesCount must not be less than zero.");
            }
            if (allCount < 0)
            {
                throw new ArgumentException("Parameter allCount must not be less than zero.");
            }

            if (positivesCount > allCount)
            {
                throw new ArgumentException("Parameter positivesCount value must not be greater than parameter allCount value.");
            }

            bool[] allBools = new bool[allCount];
            if (allCount > 0)
            {
                List<int> indices = new List<int>(Enumerable.Range(0, allCount));

                int assignedPositivesCount = 0;
                while (assignedPositivesCount < positivesCount)
                {
                    int i = rand.Next(indices.Count - 1);
                    allBools[indices[i]] = true;
                    indices.RemoveAt(i);
                    assignedPositivesCount++;
                }
            }
            return allBools;
        }
    }
}
