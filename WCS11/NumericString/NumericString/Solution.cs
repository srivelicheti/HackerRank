using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericString
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
            int xxx = 300000;
            var s = reader.ReadLine();
            //var s = new int[temp1.Length];
            //int z = 0;
            //foreach (var c in temp1)
            //{
            //    s[z] = c.AsInt();
            //    z++;
            //}

            var temp = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

            int k = (int) temp[0];
            ulong b = (ulong) temp[1];
            ulong m = (ulong) temp[2];

            int l = s.Length;

            ulong sum = 0;
            for (int i = l - 1, p = 0; i >= l - k; i-- ,p++)
            {
                sum = sum + ((ulong)s[i].AsInt()* (ulong)Math.Pow(b, p));
            }
            ulong modSum = sum%m;
            long pow = k - 1;
            for (int i = l - k - 1, toRem = l - 1; i >= 0; i--, toRem--)
            {
                sum = s[i].AsULong()*(ulong) (Math.Pow(b, pow)) + (ulong) ((sum - s[toRem].AsULong())/b);
                modSum += sum%m;
            }

            Console.WriteLine(modSum);

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        
    }

    public static class Ex {
        public static int AsInt(this char c)
        {
            return Convert.ToInt32(char.GetNumericValue(c));
        }

        public static ulong AsULong(this char c)
        {
            return (ulong) char.GetNumericValue(c);
        }
    }
}
