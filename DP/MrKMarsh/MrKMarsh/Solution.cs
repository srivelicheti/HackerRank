using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKMarsh
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
            var temp = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            var rows = temp[0];
            var cols = temp[1];

            var arr = new int[rows][];
            var lArr = new List<int>[rows][];
            var bArr = new List<int>[rows][];
            var tempR = rows;

            while (tempR > 0)
            {
                arr[tempR] = reader.ReadLine().Split(' ').Select(x => x == "." ? 0 : -1).ToArray();
                var l = new List<int>();
                for (int z = 1; z < cols; z++)
                {
                    
                }
                tempR--;
            }
             

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
