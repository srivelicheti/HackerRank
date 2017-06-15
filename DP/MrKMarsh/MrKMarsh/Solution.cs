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
            Console.WriteLine("Completed");
            Console.ReadLine();
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
            int k = 0;
            while (tempR-- > 0)
            {
                arr[k] = reader.ReadLine().Select(x => x == '.' ? 0 : -1).ToArray();
                k++;
            }

            int maxArea = 0;
            for (int i1 = 0; i1 < rows; i1++)
            {
                for (int i2 = i1 + 1; i2 < rows; i2++)
                {
                    int j1 = -1;
                    int j2 = -1;
                    for (int j = 0; j < cols; j++)
                    {
                        if (arr[i1][j] != -1 && arr[i2][j] != -1)
                        {
                            if (j1 == -1)
                                j1 = j;
                            else
                                j2 = j;
                        }
                        else
                        {
                            j1 = -1;
                            j2 = -1;
                        }
                    }
                    if (j1 != -1 && j2 != -1)
                    {
                        int area = 2*((i2 - i1) + (j2 - j1));
                        maxArea = Math.Max(area, maxArea);
                    }
                }    
            }
             
            writer.WriteLine(maxArea !=0 ? maxArea.ToString() : "impossible");
            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
