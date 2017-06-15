using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternCount
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
            int n = Convert.ToInt32(reader.ReadLine());

            while (n-- > 0)
            {
                SolveONe();
                writer.Flush();
            }
            
#if DEBUG
            writer.Close();
#endif
        }

        private static void SolveONe()
        {
            string s = reader.ReadLine();

            bool isInSeq = false;
            int seqCount = 0;
            int len = s.Length;
            for (int i = 0; i < s.Length; i++)
            {
                var curChar = s[i];
                if (curChar != '0' && curChar != '1')
                {
                    isInSeq = false;
                    continue;
                }
                else if (curChar == '1')
                {
                      if (isInSeq) { 
                        seqCount++;
                          isInSeq = false;
                      }
                    if (i < len - 1 && s[i+1] == '0')
                    {
                        isInSeq = true;
                    }
                }
            }
            writer.WriteLine(seqCount);
        }
    }
}
