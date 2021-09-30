using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Compulsory
{
    public class PrimeGenerator
    {
        public List<long> GetPrimesSequential(long first, long last)
        {
            List<long> list = new List<long>();
            bool hasBeenFound = false;
            if(first == 0)
            {
                first = 1;
            }

            for (long i = first; i <= last; i++)
            {
                

                hasBeenFound = false;
                if(i % 2 == 0)
                {
                    continue;
                }

                for (long j = 3; j < i; j++)
                {
                   if(i%j == 0 && j % 2 != 0)
                    {
                        hasBeenFound = true;
                        break;
                    }
                }
                if(hasBeenFound == false)
                {
                    if(i == 1)
                    {
                        list.Add(2);
                    }
                    else
                    {
                        list.Add(i);
                    }
                   
                }
                
            }

            return list;
        }

        public List<long> GetPrimesParallel(long first, long last)
        {
            return null;
        }
    }
}
