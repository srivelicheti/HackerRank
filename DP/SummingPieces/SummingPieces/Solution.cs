using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummingPieces
{
    //https://www.hackerrank.com/challenges/summing-pieces
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
            writer = Console.Out; // new StreamWriter(Console.OpenStandardOutput());
#endif
            Solve();
#if DEBUG
            Console.WriteLine("Completed");
            Console.ReadLine();
#endif
        }

        private static int mod = (int)Math.Pow(10, 9) + 7;
        static void Solve()
        {
            reader.ReadLine();

            var arr = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

            var cArray = new int[arr.Length];
            var sArray = new int[arr.Length];
            var runnSum = new int[arr.Length];

            cArray[0] = 1;
            sArray[0] = arr[0] % mod;
            runnSum[0] = sArray[0];

            for (int i = 1; i < arr.Length; i++)
            {
                int currSum = 0;
                int currCount = 0;
                runnSum[i] = (runnSum[i - 1] % mod + arr[i])%mod;
                for (int j = i - 1; j >= 0; j--)
                {
                    currCount = (currCount + cArray[j] % mod) % mod;
                    currSum += (((runnSum[i] - runnSum[j])%mod * (i - j)%mod) % mod + sArray[j]) % mod;
                }

                sArray[i] = (currSum + (runnSum[i] * (i + 1)) % mod) % mod;
                cArray[i] = (currCount + 1)%mod;

            }

            writer.WriteLine(sArray[sArray.Length - 1]);

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
