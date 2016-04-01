using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamAndSubString
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
            var mod = Convert.ToInt64( Math.Pow(10, 9) + 7);
            var str = reader.ReadLine();
            long totalCount = 0;
            long l = 1;
            for (int i = str.Length -1; i >= 0; i--)
            {
                var c = (int) System.Char.GetNumericValue(str[i]);
                var t = (c * (i + 1) * l ) % mod;

                totalCount = (totalCount + t) % mod;
                l = (l * 10 + 1) % mod;
            }
            writer.WriteLine(totalCount);
            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
