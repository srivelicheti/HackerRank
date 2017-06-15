using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XorSum
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

            var a = reader.ReadLine().Reverse().Select(x => (int) char.GetNumericValue(x)).ToArray();
            var b = reader.ReadLine().Reverse().Select(x => (int)char.GetNumericValue(x)).ToArray();

            int n0 = a[0] == 0 ? 1 : 0;
            int n1 = a[0] == 0 ? 0 : 1;
            int lim = 314160;
            int modMul = 1;
            int total =  a[0] ^ b[0];
            
            int mod = (int)Math.Pow(10, 9) + 7;

             total = total +  (a[0] * lim) % mod;

            var maxLenth = Math.Max(a.Length, b.Length);
            for (int i = 1; i < maxLenth; i++)
            {
                var ac = i < a.Length ? a[i] : 0;
                var bc = i < b.Length ? (b[i]): 0;
                //int cont = 0;
                if (bc == 0) n0++;
                else n1++;

                int cont = ac == 0 ? n1 : n0;

                modMul = (modMul*2)%mod;

                total = (total + ((cont*modMul)%mod))%mod;

                if (ac == 1)
                    total = (((lim - i - 1)*modMul)%mod + total)%mod;
            }

            writer.WriteLine(total%mod);


            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
