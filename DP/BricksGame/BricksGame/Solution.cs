using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksGame
{
    class Solution
    {
        protected static TextReader reader;
        protected static TextWriter writer;

        static void Main(string[] args)
        {
#if DEBUG
            reader = new StreamReader(@"..\..\input.txt");
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
              writer.WriteLine(  SolveONe());
            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static ulong SolveONe()
        {
            var n = Convert.ToInt32(reader.ReadLine());
            var arr = reader.ReadLine().Split(' ').Select(x => Convert.ToUInt64(x)).Reverse().ToArray();
            if (n <= 3)
                return Sum(arr);

            ulong[] res = new ulong[arr.Length];
            short[] bCount = new short[arr.Length];

            res[0] = arr[0];
            res[1] = arr[1] + arr[0];
            res[2] = arr[2] + arr[1] + arr[0];
            bCount[0] = 1;
            bCount[1] = 2;
            bCount[2] = bCount[3] = 3;

            res[3] = arr[3] + arr[2] + arr[1];

            for (int i = 4; i < n; i++)
            {
                ulong b1 = arr[i] + (i - 1 - bCount[i - 1] >= 0 ? res[i - 1 - bCount[i - 1]] : 0);
                ulong b2 = arr[i] + arr[i - 1] + (i - 2 - bCount[i - 2] >= 0 ? res[i - 2 - bCount[i - 2]] : 0);
                ulong b3 = arr[i] + arr[i - 1] + arr[i - 2] + (i - 3 - bCount[i - 3] >= 0 ? res[i - 3 - bCount[i - 3]] : 0);

                ulong max = b1;
                bCount[i] = 1;
                if (b2 > max)
                {
                    max = b2;
                    bCount[i] = 2;
                }
                if (b3 > max)
                {
                    max = b3;
                    bCount[i] = 3;
                }

                res[i] = max;
            }

           // foreach (var s in bCount)
           // {
           //     Console.Write(s + " ");
           // }
           //// Console.WriteLine();
            return res[n-1];

        }

        private static ulong Sum(ulong[] arr)
        {
            ulong s = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                s = s + arr[i];
            }

            return s;
        }   

        private static ulong Min(ulong i1, ulong i2, ulong i3)
        {
            ulong min = i1;
            min = i1 > i2 ? i2 : i1;
            min = min > i3 ? i3 : min;
            return min;
        }

    }
}
