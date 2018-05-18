using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSaper.model
{
    public class RandomBooleansGenerator
    {
        public bool[] GenerateRandomBooleans(int allCount, int positivesCount)
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
            Random rand = new Random(DateTime.Now.Millisecond);

            int assignedPositivesCount = 0;
            do
            {
                int i = rand.Next(allCount);
                if (!allBools[i])
                {
                    allBools[i] = true;
                    assignedPositivesCount++;
                }
            }
            while (assignedPositivesCount < positivesCount);

            return allBools;
        }
    }
}
