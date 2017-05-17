using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerLandRadioTransmitters
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
            writer.WriteLine("Completed");
            Console.ReadLine();
#endif
        }

        static void Solve()
        {
            var temp = reader.ReadLine().Split(' ');
            var n = Convert.ToInt32(temp[0]);
            var k = Convert.ToInt32(temp[1]);
            var arr = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToList();
            arr.Sort();

            int noOfTowers = 0;
            int preTowerPos = -1;
            for (int i = 0; i < n; i++)
            {
                int house = arr[i];
                if (preTowerPos != -1 && house <= preTowerPos + k)
                    continue;
                var maxTowerPos = house + k;
                if (i == n - 1) //If last house
                    noOfTowers++;
                else
                {
                    var t = i;
                    while (t + 1 < n && arr[t + 1] <= maxTowerPos)
                    {
                        t++;
                    }
                    i = t;
                    preTowerPos = arr[i];
                    noOfTowers++;
                }

            }
            writer.WriteLine(noOfTowers);


            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
