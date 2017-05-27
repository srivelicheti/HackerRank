using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubStringDiff
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
            var t = Convert.ToInt32(reader.ReadLine());
            while (t-- > 0)
            {
                SolveOne();
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static void SolveOne()
        {
            var temp = reader.ReadLine().Split(' ');
            var k = Convert.ToInt32(temp[0]);

            var s1 = temp[1];
            var s2 = temp[2];

            var lcs = new int[s1.Length + 1, s2.Length + 1];
            //for (int i = 0; i < s2.Length; i++)
            //{
            //    lcs[i, 0] = 0;
            //    lcs[0, i] = 0;
            //}

            int currMax = 0;
            var maxEnds = new List<Tuple<int,int,int>>();
            //var maxEnd = new Tuple<int, int>(0, 0);

            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
                        if (lcs[i, j] > currMax)
                        {
                            currMax = lcs[i, j];
                           // maxEnds.Clear();
                            //maxEnd = new Tuple<int, int>(i, j);
                            maxEnds.Add(new Tuple<int, int,int>(i, j,currMax));
                        }
                        else if (lcs[i, j] == currMax)
                        {
                           maxEnds.Add(new Tuple<int, int,int>(i, j,currMax));
                        }
                    }
                }
            }

            int maxLen = 0;

            foreach (var maxEnd in maxEnds)
            {
                var s1Start = maxEnd.Item1 - currMax - 1;
                var s2Start = maxEnd.Item2 - currMax - 1;
                var misMatch = 0;
                int lMaxLength = 0;
                var lMisMatchArray = new int[k + 1];
                while (s1Start >= 0 && s2Start >= 0)
                {
                    if (s1[s1Start--] != s2[s2Start--])
                    {
                        if (misMatch == k)
                            break;

                        misMatch++;
                    }

                    lMaxLength++;
                    lMisMatchArray[misMatch] = lMaxLength;
                }
                for (int i = 1; i < lMisMatchArray.Length; i++)
                {
                    if (lMisMatchArray[i] == 0)
                        lMisMatchArray[i] = lMisMatchArray[i - 1];
                }



                var s1End = maxEnd.Item1;
                var s2End = maxEnd.Item2;

                int rMisMatch = 0;
                int rMaxLenth = 0;
                var rMisMatchArray = new int[k + 1];
                while (s1End < s1.Length && s2End < s2.Length)
                {
                    if (s1[s1End++] != s2[s2End++])
                    {
                        if (rMisMatch == k)
                            break;

                        rMisMatch++;
                    }
                    rMaxLenth++;
                    rMisMatchArray[rMisMatch] = rMaxLenth;
                }
                for (int i = 1; i < rMisMatchArray.Length; i++)
                {
                    if (rMisMatchArray[i] == 0)
                        rMisMatchArray[i] = rMisMatchArray[i - 1];
                }
                
                for (int i = 0; i < lMisMatchArray.Length; i++)
                {
                    var remMis = k - i;
                    var max = rMisMatchArray[remMis] + lMisMatchArray[i] + maxEnd.Item3;
                    maxLen = Math.Max(maxLen, max);
                }
                for (int i = 0; i < rMisMatchArray.Length; i++)
                {
                    var remMis = k - i;
                    var max = lMisMatchArray[remMis] + rMisMatchArray[i] + maxEnd.Item3;
                    maxLen = Math.Max(maxLen, max);
                }

              
            }
            writer.WriteLine(maxLen);

        }

        private static bool Equal(string s1, int i, string s2, int j)
        {
            char x = i >= 0 ? s1[i] : ' ';
            char y = j >= 0 ? s2[j] : ' ';
            return x == y;
        }
    }
}
