using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryChild2
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
            writer = Console.Out; // new StreamWriter(Console.OpenStandardOutput());
#endif
            Solve();
#if DEBUG
            Console.WriteLine("Completed");
            Console.ReadLine();
#endif
        }

        static void Solve()
        {
           // SortedSet<UInt64> sortedSet = new SortedSet<UInt64>();
            
            var totalPackets = Convert.ToInt32(reader.ReadLine());
            var toDistribute = Convert.ToInt32(reader.ReadLine());
            var packets = new UInt64[totalPackets];
            for (var i = 0; i < totalPackets; i++)
            {
                var packSize = Convert.ToUInt64(reader.ReadLine());
                //sortedSet.Add(packSize);
                packets[i] = packSize;
            }
            Array.Sort(packets);
            var selectedPackets = packets.Take(toDistribute).ToArray();
           
            var sumArray = new UInt64[toDistribute];
            sumArray[0] = selectedPackets[0];
            for (var i = 1; i < toDistribute; i++)
            {
                sumArray[i] = sumArray[i - 1] + selectedPackets[i];
            }

            UInt64 totalUnfariness = 0;
            if (selectedPackets.Length > 1)
            {
                for (long i = 1; i < selectedPackets.Length; i ++)
                {
                    totalUnfariness = totalUnfariness + (((UInt64) selectedPackets[i] * (UInt64) i) - sumArray[i -1] );
                }
            }

            var mxSum = sumArray[toDistribute - 1];
            ulong c = (ulong) (toDistribute - 1);
            for (long i = toDistribute; i < totalPackets; i ++)
            {
                ulong temp = mxSum;
                mxSum = mxSum + packets[i];
                mxSum = mxSum - packets[i - toDistribute];

                var tempUnfairness = packets[i]*c - mxSum;
                if (tempUnfairness < totalUnfariness)
                    totalUnfariness = tempUnfairness;

            }

            writer.WriteLine(totalUnfariness);
            writer.Flush();
            
#if DEBUG
            writer.Close();
#endif
        }
    }
}
