using System;
using System.Collections.Generic;

namespace Parallel_Compulsory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            PrimeGenerator pg = new PrimeGenerator();
            List<long> list = pg.GetPrimesSequential(0, 100);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
