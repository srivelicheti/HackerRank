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
            var s = reader.ReadLine();
           
            var temp = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

            int k = (int) temp[0];
            int b = (int) temp[1];
            int m = (int) temp[2];

            int l = s.Length;

            long sum = 0;
            long toDelSum = 0;
            for (int i = l - 1, p = 0; i >= l - k; i-- ,p++)
            {
                var cm = s[i].AsInt()%m;
                var t = (cm*ModPow(b, p, m))%m;
                sum = sum + t;
                if (i != l - 1)
                {
                    toDelSum = toDelSum + ( t - ((cm * ModPow(b, p-1, m)) % m)) ;
                }
            }
            long modSum = sum%m;
            int pow = k - 1;
            
            long zzz = ModPow(b,pow,m);
            int zzzz = ModPow(b, pow - 1, m);
            for (int i = l - k - 1, toRem = l - 1; i >= 0; i--, toRem--)
            {
                var cm = (s[i].AsInt() % m);
                var rem = s[toRem].AsInt()%m;
                var t = (cm*zzz)%m;
                var befSum = (sum - rem - toDelSum);
                sum = t + (befSum);
                modSum += sum%m;
                toDelSum = toDelSum + (t - ((cm * zzzz) % m));
            }

            Console.WriteLine(modSum);

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        static int ModPow(int b, int p, int m)
        {
            int res = 1;
            int t = b%m;
            while (p-- > 0)
            {
                res = (res*t)%m;
            }

            return res;
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
