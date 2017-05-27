using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSubArray
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
            writer = Console.out; // new StreamWriter(Console.OpenStandardOutput());
#endif
            Solve();
#if DEBUG
            Console.WriteLine("Completed");
            Console.ReadLine();
#endif
        }

        static void Solve()
        {
            var n = Convert.ToInt32(reader.ReadLine());
            while (n-- > 0)
            {
                SolveOne();
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static void SolveOne()
        {
            reader.ReadLine();

            var arr = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

            int maxSum = 0;
            int currentSum = 0;
            int nonContMaxSum = 0;
            foreach (var i in arr)
            {
                if (i > 0)
                    nonContMaxSum += i;
                currentSum += i;
                if (currentSum <= 0)
                    currentSum = 0;
                else if (currentSum > maxSum)
                    maxSum = currentSum;

            }
            writer.WriteLine(maxSum+ " "+ nonContMaxSum);
        }
    }
}
