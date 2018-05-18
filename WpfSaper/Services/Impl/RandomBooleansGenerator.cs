using System;
using WpfSaper.services;

namespace WpfSaper.Services.Impl
{
    public class RandomBooleansGenerator : IBooleansGenerator
    {
        private readonly Random rand = new Random(DateTime.Now.Millisecond);

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
            int assignedPositivesCount = 0;

            while (assignedPositivesCount < positivesCount)
            {
                int i = rand.Next(allCount);
                if (!allBools[i])
                {
                    allBools[i] = true;
                    assignedPositivesCount++;
                }
            }           
            return allBools;
        }
    }
}
