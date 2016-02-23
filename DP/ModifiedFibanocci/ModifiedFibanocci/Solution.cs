using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace ModifiedFibanocci
{
    class Solution
    {
        protected static TextReader reader;
        protected static TextWriter writer;

        static void Main(string[] args)
        {
#if DEBUG
            reader = new StreamReader("..\\..\\input.txt");
            writer = Console.Out;
           //  writer = new StreamWriter("..\\..\\output.txt");
#else
            reader = Console.In;
            writer = new StreamWriter(Console.OpenStandardOutput());
#endif
            Solve();
            
#if DEBUG
            Console.WriteLine("Completed");
            Console.ReadLine();
#endif
        }

        private static BigInteger[] cache;
        static void Solve()
        {

            var parts = reader.ReadLine().Split(' ').Select(x => Convert.ToUInt64(x)).ToArray();
            var toFind = Convert.ToInt32(parts[2]);
            cache = new BigInteger[toFind];
            //for (int i = 0; i < toFind; i++)
            //    cache[i] = -1;
            cache[0] = parts[0];
            cache[1] = parts[1];
            //for (int i = 2; i < Convert.ToInt32(toFind); i++)
            //{
            //    cache[i] = GetAtIndex(i);
            //}
            
            writer.WriteLine(GetAtIndex(toFind - 1));
            writer.Flush();
        }

        static BigInteger GetAtIndex(int n)
        {
            if (n == 0 || n == 1)
                return cache[n];
            else if (cache[n] != 0)
                return cache[n];
            else
            {
                cache[n] = (BigInteger.Pow(GetAtIndex(n - 1), 2)) + GetAtIndex(n - 2);
                return cache[n];
            }
        }
    }
}
