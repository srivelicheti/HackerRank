using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimumLoss
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
            reader.ReadLine();

            var prices = reader.ReadLine().Split(' ').Select(x => Convert.ToInt64(x));

            var sortedDic = new List<Tuple<long, int>>();
            int i = 0;
            foreach (var p in prices)
            {
                sortedDic.Add(new Tuple<long, int>(p, i));
                i++;
            }
            sortedDic.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            i = 0;
            long lowest = Int64.MaxValue;
            for (; i < sortedDic.Count - 1; i++)
            {

                if ((sortedDic[i].Item2 > sortedDic[i + 1].Item2) & (sortedDic[i].Item1 != sortedDic[i + 1].Item1))
                {
                    var val = sortedDic[i + 1].Item1 - sortedDic[i].Item1;
                    //  Console.WriteLine(val);
                    if (val < lowest)
                        lowest = val;

                }


            }
            writer.WriteLine(lowest);
            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
