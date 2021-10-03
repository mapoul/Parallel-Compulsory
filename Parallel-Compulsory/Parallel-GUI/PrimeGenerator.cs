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
        long seqI = 0;
        int numberOfThreads = 2;
        public List<long> GetPrimesSequential(long first, long last)
        {
            List<long> list = new List<long>();
            bool hasBeenFound = false;


            for (long i = first; i <= last; i++)
            {
                seqI = i;
                hasBeenFound = false;
                if (i % 2 == 0)
                {
                    continue;
                }

                for (long j = 3; j <= (long)Math.Sqrt(i); j++)
                {
                    if (i % j == 0 && j % 2 != 0)
                    {
                        hasBeenFound = true;
                        break;
                    }
                }
                if (hasBeenFound == false)
                {
                    if (i == 1)
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

            Task[] tasks = new Task[numberOfThreads];
            for (int i = 0; i < numberOfThreads; i++)
            {
                Task t = Task.Factory.StartNew(() =>
                {
                    bool hasBeenFound = false;
                    long num = 0;
                    while ((num = GetIncrementParallelLong()) <= last)
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
                                lock (numLock)
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
            Task.WaitAll(tasks);
            list.Sort();
            return list;
        }



        private long GetIncrementParallelLong()
        {
            lock (methodLock)
            {
                long a = Interlocked.Read(ref parallelI);
                Interlocked.Increment(ref parallelI);
                return a;
            }

        }

        public long GetParallelLong()
        {
            return Interlocked.Read(ref parallelI);
        }

        public long GetSeqLong()
        {
            return Interlocked.Read(ref seqI);
        }

        public void SetThreads(int threads)
        {
            numberOfThreads = threads;
        }
    }
}
