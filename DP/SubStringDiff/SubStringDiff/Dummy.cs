//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SubStringDiff
//{

//    class Solution
//    {
//        protected static TextReader reader;
//        protected static TextWriter writer;

//        static void Main(string[] args)
//        {
//#if DEBUG
//            reader = new StreamReader("..\\..\\input.txt");
//            writer = Console.Out;
//            // writer = new StreamWriter("..\\..\\output.txt");
//#else
//            reader = Console.In;
//            writer = Console.out; // new StreamWriter(Console.OpenStandardOutput());
//#endif
//            Solve();
//#if DEBUG
//            Console.WriteLine("Completed");
//            Console.ReadLine();
//#endif
//        }

//        static void Solve()
//        {
//            var t = Convert.ToInt32(reader.ReadLine());
//            while (t-- > 0)
//            {
//                SolveOne();
//            }

//            writer.Flush();
//#if DEBUG
//            writer.Close();
//#endif
//        }

//        private static void SolveOne()
//        {
//            var temp = reader.ReadLine().Split(' ');
//            var k = Convert.ToInt32(temp[0]);

//            var s1 = temp[1];
//            var s2 = temp[2];

//            var lcs = new int[s1.Length + 1, s2.Length + 1];
//            //for (int i = 0; i < s2.Length; i++)
//            //{
//            //    lcs[i, 0] = 0;
//            //    lcs[0, i] = 0;
//            //}

//            int currMax = 0;
//            Tuple<int, int> maxEnd = new Tuple<int, int>(0, 0);

//            for (int i = 1; i <= s1.Length; i++)
//            {
//                for (int j = 1; j <= s2.Length; j++)
//                {
//                    if (s1[i - 1] == s2[j - 1])
//                    {
//                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
//                        if (lcs[i, j] > currMax)
//                        {
//                            currMax = lcs[i, j];
//                            maxEnd = new Tuple<int, int>(i, j);
//                        }
//                    }
//                }
//            }

//            var s1Start = maxEnd.Item1 - currMax - 1;
//            var s2Start = maxEnd.Item2 - currMax - 1;
//            var misMatch = 0;
//            int maxLenght = currMax;
//            while (s1Start > 0 && s2Start > 0)
//            {
//                if (s1[s1Start--] != s2[s2Start--])
//                {
//                    if (misMatch == k)
//                        break;

//                    misMatch++;
//                }

//                maxLenght++;
//            }

//            int lMaxLen = maxLenght;

//            var s1End = maxEnd.Item1;
//            var s2End = maxEnd.Item2;

//            int rMisMatch = 0;
//            int rMaxLenth = currMax;
//            while (s1End < s1.Length && s2End < s2.Length)
//            {
//                if (s1[s1End++] != s2[s2End++])
//                {
//                    if (rMisMatch == k)
//                        break;

//                    rMisMatch++;
//                }
//                rMaxLenth++;
//            }


//            while (s1End < s1.Length && s2End < s2.Length)
//            {
//                if (s1[s1End++] != s2[s2End++])
//                {
//                    if (misMatch == k)
//                        break;

//                    misMatch++;
//                }
//                maxLenght++;
//            }

//            int rlMaxle = rMaxLenth;
//            int rlMisMatch = rMisMatch;

//            while (s1End < s1.Length && s2End < s2.Length)
//            {
//                if (s1[s1End++] != s2[s2End++])
//                {
//                    if (rlMisMatch == k)
//                        break;

//                    rlMisMatch++;
//                }
//                rlMaxle++;
//            }



//            writer.WriteLine(Math.Max(rlMaxle, Math.Max(Math.Max(lMaxLen, rMaxLenth), maxLenght)));

//            //  Console.WriteLine($"currMax {currMax} MaxEnd {maxEnd}  MaxLenght {maxLenght}");

//        }

//        private static bool Equal(string s1, int i, string s2, int j)
//        {
//            char x = i >= 0 ? s1[i] : ' ';
//            char y = j >= 0 ? s2[j] : ' ';
//            return x == y;
//        }
//    }
//}
