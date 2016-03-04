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
        reader = Console.In;
        writer = Console.Out;
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
    }
    static void Solve()
    {
        var noOfDays = int.Parse(reader.ReadLine());
        var options = new int[noOfDays];
        var dailyPrices = reader.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        var currMax = dailyPrices[noOfDays - 1];
        options[noOfDays - 1] = -1;
        for (int i = noOfDays - 2; i >= 0; i--) {
            var todayPrice = dailyPrices[i];
            if (todayPrice < currMax)
            {
                options[i] = 1;
            }
            else if (todayPrice > currMax)
            {
                options[i] = -1;
                currMax = dailyPrices[i];
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
       // writer.WriteLine(string.Join(" ", options));
    }
}
