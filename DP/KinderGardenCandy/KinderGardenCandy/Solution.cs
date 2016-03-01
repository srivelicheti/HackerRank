using System;
using System.IO;
using System.Linq;


namespace KinderGardenCandy
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
            var noOfKids = Convert.ToInt32(reader.ReadLine());
            var ranks = new int[noOfKids+2];
            ranks[0] = Int32.MaxValue;
            ranks[noOfKids + 1] = Int32.MaxValue;
            for (int i = 1; i <= noOfKids; i++)
            {
                ranks[i] = Convert.ToInt32(reader.ReadLine());
            }
            var candy = new int[noOfKids+2];

            int noOfKidsDetermined = 0;
            while (noOfKidsDetermined < noOfKids)
            {
                for (int i = 1; i <= noOfKids; i++)
                {
                    if (candy[i] == 0)
                    {
                        //if (i == 0)
                        //{
                        //    if (ranks[0] < ranks[1])
                        //    {
                        //        candy[0] = 1;
                        //        noOfKidsDetermined++;
                        //    }
                        //    else if (candy[1] != 0)
                        //    {
                        //        if (ranks[0] == ranks[1])
                        //            candy[0] = 1;
                        //        else
                        //            candy[0] = candy[1] + 1;
                        //        noOfKidsDetermined++;
                        //    }
                        //}
                        //else if (i == noOfKids - 1)
                        //{
                        //    if (ranks[noOfKids - 1] < ranks[noOfKids - 2])
                        //    {
                        //        candy[noOfKids - 1] = 1;
                        //        noOfKidsDetermined++;
                        //    }
                        //    else if (candy[noOfKids - 2] != 0)
                        //    {
                        //        if (ranks[noOfKids - 1] == ranks[noOfKids - 2])
                        //            candy[noOfKids - 1] = 1;
                        //        else
                        //            candy[noOfKids - 1] = candy[noOfKids - 2] + 1;
                        //        noOfKidsDetermined++;
                        //    }
                        //}
                        //else
                        {
                            if (ranks[i] < ranks[i - 1] && ranks[i] < ranks[i + 1])
                            {
                                candy[i] = 1;
                                noOfKidsDetermined++;
                            }
                            else if (candy[i - 1] != 0 && candy[i + 1] != 0)
                            {
                                int c = 1;
                                if (ranks[i] > ranks[i - 1])
                                    c = candy[i - 1] + 1;
                                if (ranks[i] > ranks[i + 1] && c <= candy[i + 1])
                                {
                                    c = candy[i + 1] + 1;
                                }
                                candy[i] = c;
                                noOfKidsDetermined++;
                            }
                            else if (candy[i - 1] != 0 && ranks[i] <= ranks[i + 1])
                            {
                                int c = 1;
                                if (ranks[i] > ranks[i - 1])
                                    c = candy[i - 1] + 1;
                                candy[i] = c;
                                noOfKidsDetermined++;
                            }
                            else if (candy[i + 1] != 0 && ranks[i] <= ranks[i - 1])
                            {
                                int c = 1;
                                if (ranks[i] > ranks[i + 1])
                                    c = candy[i + 1] + 1;
                                candy[i] = c;
                                noOfKidsDetermined++;
                            }
                        }
                    }
                }
            }


#if DEBUG
            writer.WriteLine(string.Join(" ", ranks));
            writer.WriteLine(string.Join(" ", candy));
#endif
            writer.WriteLine(candy.Sum());
            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        static void SolveSubProb(int startPos, int endPos, int[] ranks, int[] cand)
        {

        }
    }
}
