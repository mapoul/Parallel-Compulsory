using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parallel_Compulsory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            PrimeGenerator pg = new PrimeGenerator();
            //List<long> list = pg.GetPrimesSequential(0, 100);
            List<long> listpar = pg.GetPrimesParallelConBag(1, 100);
            foreach (var item in listpar)
            {
                Console.WriteLine(item);
            }
            
        }

    }
}
