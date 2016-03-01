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
            for (int i = 0; i < noOfKids; i++)
            {
                ranks[i] = Convert.ToInt32(reader.ReadLine());
            }
            var candy = new int[noOfKids];

            int noOfKidsDetermined = 0;
            if (ranks[0] <= ranks[1])
            {
                candy[0] = 1;
            }
            if (ranks[noOfKids - 1] <= ranks[noOfKids - 2])
                candy[noOfKids - 1] = 1;

            for (int i = 0; i < noOfKids; i++)
            {
                if (candy[i] == 0)
                {

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
