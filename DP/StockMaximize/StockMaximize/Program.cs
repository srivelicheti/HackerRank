using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        //writer = new StreamWriter("..\\..\\output.txt", false);

#else
            //reader = Console.In;
        reader = new StreamReader("..\\..\\input.txt");
        writer = Console.Out; //  new StreamWriter(Console.OpenStandardOutput());
#endif

        int noOfTestCases = int.Parse(reader.ReadLine());
        for (int z = 0; z < noOfTestCases; z++)
        {
            Solve();
        }
#if DEBUG
        Console.WriteLine("Completed");
        Console.ReadLine();
#endif
        Console.WriteLine("Completed");
        Console.ReadLine();
    }
    static void Solve()
    {
        var noOfDays = int.Parse(reader.ReadLine());
        var options = new int[noOfDays];
        var dailyPrices = reader.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

        for (int i = 1; i < noOfDays; i++)
        {
            if (dailyPrices[i] > dailyPrices[i - 1])
            {
                options[i] = -1;
                int j = i-1;
                int currentPrice = dailyPrices[i];
                do
                {
                    if (dailyPrices[j] < currentPrice)
                        options[j] = 1;
                    else
                        break;
                    j--;
                } while (j >= 0);
            }
        }
        long currentProfit = 0;
        long currentCost = 0;
        long accumulatedShares = 0;
        for (int i = 0; i < noOfDays; i++)
        {
            if (options[i] == 1)
            {
                accumulatedShares++;
                currentCost = currentCost + dailyPrices[i];
            }
            else if (options[i] == -1)
            {
                currentProfit = currentProfit + ((accumulatedShares * dailyPrices[i]) - currentCost);
                accumulatedShares = 0;
                currentCost = 0;
            }
        }
        writer.WriteLine(currentProfit);
        writer.WriteLine(string.Join(" ", options));
    }
}
