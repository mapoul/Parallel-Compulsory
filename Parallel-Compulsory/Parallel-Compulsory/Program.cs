using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Parallel_Compulsory
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine((long)Math.Sqrt(10));

            long start = 0;
            long stop = 10000000;


            Console.WriteLine("Hello World!");
            PrimeGenerator pg = new PrimeGenerator();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<long> list = pg.GetPrimesSequential(start, stop);
            sw.Stop();

            Stopwatch swp = new Stopwatch();
            swp.Start();
            List<long> listpar = pg.GetPrimesParallel(start, stop);
            swp.Stop();
            Console.WriteLine($"Non parallel {start}  to  {stop} took: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"parallel {start}  to  {stop} took: {swp.ElapsedMilliseconds}ms");

        }

    }
}
