using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneEditing
{
    class Counters
    {
        public Counters(short a, short c, short g, short t)
        {
            A = a;
            C = c;
            G = g;
            T = t;
        }

        public Counters(char x)
        {
            if (x == 'A')
                A++;
            else if (x == 'C')
                C++;
            else if (x == 'G')
                G++;
            else if (x == 'T')
                T++;
        }

        public short A { get; set; }
        public short C { get; set; }
        public short G { get; set; }
        public short T { get; set; }

        public Counters Add(char x)
        {
            short a = A;
            short c = C;
            short g = G;
            short t = T;
            if (x == 'A')
                a++;
            else if (x == 'C')
                c++;
            else if (x == 'G')
                g++;
            else if (x == 'T')
                t++;

            return  new Counters(a,c,g,t);
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

        static void Solve()
        {
            string s;
            int l;
            l = Convert.ToInt32(reader.ReadLine());
            s = reader.ReadLine();

            int expCn = l / 4;

            int oaCn = 0;
            int ocCn=0;
            int ogCn=0;
            int otCn=0;
            for (int i = 0; i < l; i++)
            {
                if (s[i] == 'A')
                    oaCn++;
                else if (s[i] == 'C')
                    ocCn++;
                else if (s[i] == 'G')
                    ogCn++;
                else if (s[i] == 'T')
                    otCn++;
            }
            int tRmA = oaCn > expCn ? oaCn - expCn : 0;
            int tRmC = ocCn > expCn ? ocCn - expCn : 0;
            int tRmG = ogCn > expCn ? ogCn - expCn : 0;
            int tRmT = otCn > expCn ? otCn - expCn : 0;

            int minWinowLen = tRmA + tRmC + tRmG + tRmT;
            int minLen = -1;
            var cArr = new Counters[l, l];
            for (int gap = 0; gap < l && minLen == -1; gap++)
            {
                for (int i = 0, j = i + gap; j < l; i++ ,j++)
                {
                        if (i == j)
                        {
                            cArr[i, j] = new Counters(s[i]);
                        }
                        else
                        {
                            cArr[i, j] = cArr[i, j - 1].Add(s[j]);
                        }
                    var curCounters = cArr[i, j];
                    if ((tRmA == 0 || tRmA == curCounters.A) && (tRmC == 0 || tRmC == curCounters.C) &&
                        (tRmG == 0 || tRmG == curCounters.G) && (tRmT == 0 || tRmT == curCounters.T))
                    {
                        minLen = gap +1;
                        break;
                    }
                }
            }
            writer.WriteLine(minLen);
            //int minLen = -1;


            //do
            //{
            //    int winA = 0;
            //    int winC = 0;
            //    int winG = 0;
            //    int winT = 0;

            //    for (int i = 0; i < minWinowLen; i++)
            //    {
            //        if (s[i] == 'A')
            //            winA++;
            //        else if (s[i] == 'C')
            //            winC++;
            //        else if (s[i] == 'G')
            //            winG++;
            //        else if (s[i] == 'T')
            //            winT++;
            //    }
            //    if ((tRmA == 0 || tRmA == winA) && (tRmC == 0 || tRmC == winC) && (tRmG == 0 || tRmG == winG) && (tRmT == 0 || tRmT == winT))
            //    {
            //        minLen = minWinowLen;
            //        break;
            //    }
            //    for (int i = minWinowLen; i < l; i++)
            //    {



            //                var lc = s[i - minWinowLen];
            //                if (lc == 'A')
            //                    winA--;
            //                else if (lc == 'C')
            //                    winC--;
            //                else if (lc == 'G')
            //                    winG--;
            //                else if (lc == 'T')
            //                    winT--;
            //        if (s[i] == 'A')
            //            winA++;
            //        else if (s[i] == 'C')
            //            winC++;
            //        else if (s[i] == 'G')
            //            winG++;
            //        else if (s[i] == 'T')
            //            winT++;

            //        if ((tRmA == 0 || tRmA == winA) && (tRmC == 0 || tRmC == winC) && (tRmG == 0 || tRmG == winG) && (tRmT == 0 || tRmT == winT))
            //        {
            //            minLen = minWinowLen;
            //            break;
            //        }



            //    }
            //    minWinowLen++;
            //} while (minLen == -1);

            //writer.WriteLine(minLen);
            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
