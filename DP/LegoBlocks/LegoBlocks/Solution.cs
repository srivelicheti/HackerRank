using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoBlocks
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
            var t = Convert.ToInt32(reader.ReadLine());

            while (t-- > 0)
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
            var temp = reader.ReadLine().Split(' ');
            var n = Convert.ToInt32(temp[0]);
            var m = Convert.ToInt32(temp[1]);

            var widthPerms = new int[m + 1];
            var res = new int[n + 1, m + 1];
            widthPerms[1] = 1;

            if (m >= 2)
            {
                widthPerms[2] = 2;
                res[1, 2] = 2;
            }
            if (m >= 3)
            {
                widthPerms[3] = 4;
                res[1, 3] = 4;
            }

            if (m >= 4)
            {

                widthPerms[4] = 8;
                res[1, 4] = 8;
            }


            res[1, 1] = 1;
           
           
            

            for (int i = 5; i <= m; i++)
            {
                int total = 0;
                total += widthPerms[i - 4];
                total +=  widthPerms[i - 3] ;
                total +=  widthPerms[i - 2] ;
                widthPerms[i] = total + widthPerms[i - 1];
                res[1, i] = widthPerms[i];
            }

            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    var b = widthPerms[j];
                    var bTotal = (int) Math.Pow(b, i);
                    for (int k = 1; k < j; k++)
                    {
                        bTotal = bTotal - (res[i, k] * (int) Math.Pow(j -k , i) );
                    }

                    res[i, j] = bTotal;
                }
            }

            writer.WriteLine(res[n, m]);
        }
    }
}
