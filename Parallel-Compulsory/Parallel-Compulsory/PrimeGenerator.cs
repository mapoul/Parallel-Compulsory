using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Compulsory
{
    public class PrimeGenerator
    {

        long parallelI = 0;
        String numLock = "";
        String methodLock = "";
        

        public List<long> GetPrimesSequential(long first, long last)
        {
            List<long> list = new List<long>();
            bool hasBeenFound = false;
         

            for (long i = first; i <= last; i++)
            {
                hasBeenFound = false;
                if(i % 2 == 0)
                {
                    continue;
                }

                for (long j = 3; j <= (long)Math.Sqrt(i); j++)
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
            parallelI = first;
            List<long> list = new List<long>();
            
            int amountOfThreads = 2;
            Task[] tasks = new Task[amountOfThreads];
            for (int i = 0; i < amountOfThreads; i++)
            {
                Task t = Task.Factory.StartNew(() =>
                {
                    bool hasBeenFound = false;
                    long num = 0;
                    while((num = getParallelLong()) <= last)
                    {
                        hasBeenFound = false;
                        if (num % 2 == 0)
                        {
                            continue;
                        }
                        
                        for (long j = 3; j <= (long)Math.Sqrt(num); j++)
                        {
                            if (num % j == 0 && j % 2 != 0)
                            {
                                hasBeenFound = true;
                                break;
                            }
                        }
                        if (hasBeenFound == false)
                        {
                            if (num == 1)
                            {
                                lock  (numLock)
                                {
                                    list.Add(2);
                                }
                                
                            }
                            else
                            {
                                lock (numLock)
                                {
                                    list.Add(num);
                                }
                                
                            }

                        }

                    }
                }
                );
                tasks[i] = t;
            }
            Console.WriteLine("Waiting for tasks");
            Task.WaitAll(tasks);
            list.Sort();
            //list.AddRange(con);
            return list;
        }


        public List<long> GetPrimesParallelConBag(long first, long last)
        {
            parallelI = first;
            List<long> list = new List<long>();
            ConcurrentBag<long> con = new ConcurrentBag<long>();

            int amountOfThreads = 2;
            Task[] tasks = new Task[amountOfThreads];
            for (int i = 0; i < amountOfThreads; i++)
            {
                Task t = Task.Factory.StartNew(() =>
                {
                    bool hasBeenFound = false;
                    long num = 0;
                    while ((num = getParallelLong()) <= last)
                    {

                        hasBeenFound = false;
                        if (num % 2 == 0)
                        {
                            continue;
                        }

                        for (long j = 3; j <= (long)Math.Sqrt(num); j++)
                        {
                            if (num % j == 0 && j % 2 != 0)
                            {
                                hasBeenFound = true;
                                break;
                            }
                        }
                        if (hasBeenFound == false)
                        {
                            if (num == 1)
                            {
                                
                                con.Add(2);
                            }
                            else
                            {
                                con.Add(num);
                            }

                        }

                    }
                }
                );
                tasks[i] = t;
            }
            Console.WriteLine("Waiting for tasks");
            Task.WaitAll(tasks);
            list = con.ToList();
            list.Sort();
            //list.AddRange(con);
            return list;
        }



        private long getParallelLong()
        {
            lock (methodLock)
            {
                long a = Interlocked.Read(ref parallelI);
                Interlocked.Increment(ref parallelI);
                return a;
            }
            
        }
    }
}
