using System;
using System.IO;
using System.Linq;

namespace CoinChange2
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

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
            var temp = reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var amount = Convert.ToInt32(temp[0]);
            var num = Convert.ToInt32(temp[1]);

            var coins = reader.ReadLine().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToArray();
            writer.WriteLine(count2(coins, num, amount));
        }

        static long count2(int[] S, int m, int n)
        {
            long i, j, x, y;

            // We need n+1 rows as the table is consturcted in bottom up manner using 
            // the base case 0 value case (n = 0)
            var table = new long[n + 1, m];

            // Fill the enteries for 0 value case (n = 0)
            for (i = 0; i < m; i++)
                table[0, i] = 1;

            // Fill rest of the table enteries in bottom up manner  
            for (i = 1; i < n + 1; i++)
            {
                for (j = 0; j < m; j++)
                {
                    // Count of solutions including S[j]
                    x = (i - S[j] >= 0) ? table[i - S[j], j] : 0;

                    // Count of solutions excluding S[j]
                    y = (j >= 1) ? table[i, j - 1] : 0;

                    // total count
                    table[i, j] = x + y;
                }
            }
            return table[n, m - 1];
        }

        static long count(int[] S, int m, int n)
        {
            // If n is 0 then there is 1 solution (do not include any coin)
            if (n == 0)
                return 1;

            // If n is less than 0 then no solution exists
            if (n < 0)
                return 0;

            // If there are no coins and n is greater than 0, then no solution exist
            if (m <= 0 && n >= 1)
                return 0;

            // count is sum of solutions (i) including S[m-1] (ii) excluding S[m-1]
            return count(S, m - 1, n) + count(S, m, n - S[m - 1]);
        }
    }
}
