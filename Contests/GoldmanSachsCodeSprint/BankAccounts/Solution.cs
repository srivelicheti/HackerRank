using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts
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
            //Console.WriteLine("Completed");
            //Console.ReadLine();
#endif
        }

        static void Solve()
        {
            int q = Convert.ToInt32(reader.ReadLine());

            while (q-- > 0)
            {

                var temp = reader.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                int n = temp[0];
                decimal k = temp[1];
                decimal x = temp[2];
                decimal d = temp[3];

                List<int> transactions = reader.ReadLine().Split(' ').Select(Int32.Parse).ToList();

                decimal total = 0;
                for (int i = 0; i <n ;i++)
                {
                    total = total + Math.Max(k, (x*transactions[i])/(decimal) 100);
                    if (total >= d)
                    {
                        break;
                    }
                }

                if(total <= d)
                    writer.WriteLine("fee");
                else
                    writer.WriteLine("upfront");
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
