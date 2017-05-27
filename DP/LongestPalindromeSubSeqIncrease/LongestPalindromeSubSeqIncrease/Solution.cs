using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPalindromeSubSeqIncrease
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
            var temp = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            var n = temp[0];
            var k = temp[1];

            var str = reader.ReadLine(); //.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

        }


        private static void GetMaxPalindromeSubSeq(int n, string str)
        {
            var lIndexes = new string[str.Length, str.Length];
            var l = new int[str.Length, str.Length];
            int longest = 1;

            for (int i = 0; i < str.Length; i++)
            {
                l[i, i] = 1;
                lIndexes[i, i] = str[i].ToString();
            }

            for (int k = 1; k < n; k++)
            {
                for (int i = 0, j = i + k; j < n; i++, j++)
                {

                    if (str[i] == str[j])
                    {
                        
                    }
                }
            }


        }

    }
}
