using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RedJohnIsBack
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
            // writer = new StreamWriter("..\\..\\output.txt");
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

        static void Solve()
        {
            _cache[1] = 1;
            _cache[4] = 2;
            var noOfTestCases = int.Parse(reader.ReadLine());
            
            for (int z = 0; z < noOfTestCases; z++)
            {
                var n = Convert.ToInt32(reader.ReadLine());
                var totalConfigs = GetConfigs(n);
                writer.WriteLine(Primes(totalConfigs).Count());
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static int _totalConfigs;
        private static Dictionary<int,int> _cache = new Dictionary<int, int>(); 
        static int GetConfigs(int n)
        {
            if (n < 0)
                return 0;
            else if (n == 0)
                return 1;
            if (_cache.ContainsKey(n))
                return _cache[n];
            int tempConfigs = 0;
            var horizontalConfigs = GetConfigs(n - 4);
            var verticalConfigs = GetConfigs(n - 1);
            if (horizontalConfigs > 0)
                tempConfigs = tempConfigs + horizontalConfigs;
            if (verticalConfigs > 0)
                tempConfigs = tempConfigs + verticalConfigs;
            _cache[n] = tempConfigs;
            return tempConfigs;

        }

        public static IEnumerable<int> Primes(int bound)
        {
            if (bound < 2) yield break;
            //The first prime number is 2
            yield return 2;

            BitArray composite = new BitArray((bound - 1) / 2);
            int limit = ((int)(Math.Sqrt(bound)) - 1) / 2;
            for (int i = 0; i < limit; i++)
            {
                if (composite[i]) continue;
                //The first number not crossed out is prime
                int prime = 2 * i + 3;
                yield return prime;
                //cross out all multiples of this prime, starting at the prime squared
                for (int j = (prime * prime - 2) >> 1; j < composite.Count; j += prime)
                {
                    composite[j] = true;
                }
            }
            //The remaining numbers not crossed out are also prime
            for (int i = limit; i < composite.Count; i++)
            {
                if (!composite[i]) yield return 2 * i + 3;
            }
        }
    }
}
