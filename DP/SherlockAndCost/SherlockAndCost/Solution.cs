using System;
using System.IO;
using System.Linq;

namespace SherlockAndCost
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
            //Console.WriteLine("Completed");
            //Console.ReadLine();
#endif
           
        }

        static void Solve()
        {

            var noOfTests = int.Parse(reader.ReadLine());

            while (noOfTests > 0)
            {
               Console.WriteLine( SolveOne());
                noOfTests--;
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static int SolveOne()
        {
            var n = Convert.ToInt32(reader.ReadLine());
            var arr = Console.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            var minArr = new int[n];
            var maxArr = new int[n];
            minArr[0] = 0;
            maxArr[0] = 0;

            for (int i = 1; i < n; i++)
            {
                maxArr[i] = Math.Max(Math.Abs(arr[i] - arr[i - 1]) + maxArr[i - 1],
                    Math.Abs(arr[i] - 1) + minArr[i - 1]);

                minArr[i] = Math.Max(Math.Abs(1 - arr[i-1]) + maxArr[i-1],
                         minArr[i-1]);

            }

            return Math.Max(maxArr[n - 1], minArr[n - 1]);

        }
    }
}
