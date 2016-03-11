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
            var ranks = new int[noOfKids];
            //ranks[0] = Int32.MaxValue;
            //ranks[noOfKids + 1] = Int32.MaxValue;
            for (int i = 0; i < noOfKids; i++)
            {
                ranks[i] = Convert.ToInt32(reader.ReadLine());
            }
            var candy = new int[noOfKids];

            int noOfKidsDetermined = 0;
            candy[0] = 1;
          //  while (noOfKidsDetermined < noOfKids)
            {
                for (int i = 1; i < noOfKids; i++)
                {
                    if (ranks[i] <= ranks[i -1])
                    {
                        candy[i] = 1;
                    }
                    else
                    {
                        candy[i] = candy[i - 1] + 1;
                    }
                }

                for (int j = noOfKids - 1; j >= 1; j--)
                {
                    if (ranks[j - 1] > ranks[j] && candy[j - 1] <= candy[j])
                        candy[j - 1] = candy[j] + 1;
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
