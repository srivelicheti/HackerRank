using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderProfit
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
            int q = Convert.ToInt32(reader.ReadLine());

            while (q--  > 0)
            {
                int k = Convert.ToInt32(reader.ReadLine());
                int n = Convert.ToInt32(reader.ReadLine());
                var arr = reader.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

                var mem = new int[k + 1, n+1];
                for (int i = 0; i <= n; i++)
                {
                    //If there are 0 transactions then no money can be made
                    mem[0, i] = 0;
                }

                for (int i = 0; i <= k; i++)
                {
                    //If there is only zero or one day then no money can be made
                    mem[i, 0] = 0;
                    mem[i, 1] = 0;
                }
                long maxProf = 0;
                for (int i = 1; i <= k; i++)
                {
                    for (int j = 2; j <= n; j++)
                    {
                        int curMax = 0;
                        for (int z = j - 1; z >= 1; z--)
                        {
                            if (arr[z - 1] < arr[j - 1]) // We can make some profit
                            {
                                int prof = arr[j - 1] - arr[z - 1];
                                curMax = Math.Max(curMax, (prof + mem[i - 1, z - 1]));
                            }
                        }
                        mem[i, j] = Math.Max(curMax, mem[i, j - 1]);
                        maxProf = Math.Max(maxProf, mem[i, j]);
                    }
                }
                writer.WriteLine(maxProf);
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
