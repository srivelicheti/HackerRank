using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace BankStock
{
    class TupleComparer : IComparer<Tuple<int, int>>
    {
        public int Compare(Tuple<int, int> x, Tuple<int, int> y)
        {
            var costComp = x.Item1.CompareTo(y.Item1);
            if (costComp != 0)
                return costComp;

            var dayComp = x.Item2.CompareTo(y.Item2);
            if (dayComp == 0)
                return dayComp;

            return dayComp == 1 ? -1 : 1;
        }
    }
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

        static long buyMaximumProducts(int n, long k, int[] a)
        {
            // Complete this function

            var tuples = new Tuple<int, int>[n];
            for (int i = 0; i < n; i++)
            {
                tuples[i] = new Tuple<int, int>(a[i], i + 1);
            }

            var comparer = new TupleComparer();
            Array.Sort(tuples,comparer);
            long rem = k;
            long ts = 0;
            for (int i = 0; i < n && rem>0; i++)
            {
                var data = tuples[i];
                long price = (long) data.Item1;
                int maxStocks = data.Item2;
                long costToBuyMax = price*maxStocks;
                if (costToBuyMax <= rem)
                {
                    ts += maxStocks;
                    rem -= costToBuyMax;
                }
                else
                {
                    long canBuy = rem/price;
                    ts += canBuy;
                    rem -= (canBuy*price);
                }
            }
            return ts;
        }


        static void Solve()
        {

            int n = Convert.ToInt32(reader.ReadLine().Trim());
            string[] arr_temp = reader.ReadLine().Trim().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            int[] arr = arr_temp.Select(x => Convert.ToInt32(x)).ToArray();
            long k = Convert.ToInt64(reader.ReadLine());
            long result = buyMaximumProducts(n, k, arr);
            Console.WriteLine(result);

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
