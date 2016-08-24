using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


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
        writer.WriteLine("Completed");
        Console.ReadLine();
#endif

    }

    static void Solve()
    {
        var noOfTestCases = Convert.ToInt32(reader.ReadLine());
        for (int i = 0; i < noOfTestCases; i++)
        {

            var noOfElements = Convert.ToInt32(reader.ReadLine());
            var readLine = reader.ReadLine();
            var arr = (readLine.Split(' ').Select(x => Convert.ToInt64(x)).ToArray());
            Array.Sort(arr);
            var total = arr.Sum();
            var currentStrenth = 1;
            long? max = null;
            for (int j = 0; j < arr.Length; j++)
            {
                total = total - arr[j];
                var temp = currentStrenth * (arr[j] + total);
                if ((currentStrenth + 1) * total > temp)
                    currentStrenth++;
                else
                {
                    max = temp;
                    break;
                }
            }
            writer.WriteLine(max.Value);

        }

    }

}


