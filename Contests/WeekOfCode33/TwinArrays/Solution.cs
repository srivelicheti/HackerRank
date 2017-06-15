using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinArrays
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
        static IComparer<Tuple<int,int>> comparer = new TupleComparer();
        static void Solve()
        {

            int n = Convert.ToInt32(reader.ReadLine());
            int i = 0;
            Tuple<int, int>[] ar1 = reader.ReadLine().Split(' ').Select(x => new Tuple<int, int>(int.Parse(x), i++)).ToArray();
            i = 0;
            Tuple<int, int>[] ar2 = reader.ReadLine().Split(' ').Select(x => new Tuple<int, int>(int.Parse(x), i++)).ToArray();
            int result = twinArrays(ar1, ar2);
            writer.WriteLine(result);

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        static int twinArrays(Tuple<int, int>[] ar1, Tuple<int, int>[] ar2)
        {
            //Array.Sort();
            Array.Sort(ar1,comparer);
            Array.Sort(ar2,comparer);


            if (ar1[0].Item2 != ar2[0].Item2)
                return ar1[0].Item1 + ar2[0].Item1;
            else
                return Math.Min(ar1[0].Item1 + ar2[1].Item1, ar1[1].Item1 + ar2[0].Item1);
        }

        
    }

    class TupleComparer : IComparer<Tuple<int, int>>
            {
                public int Compare(Tuple<int, int> x, Tuple<int, int> y)
                {
                    return x.Item1.CompareTo(y.Item1);
                }
        }
}
