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
        writer.WriteLine("Completed");
        Console.ReadLine();
    }

    static void Solve()
    {
        var noOfTestCases = Convert.ToInt32(reader.ReadLine());
        //ranks[0] = Int32.MaxValue;
        //ranks[noOfKids + 1] = Int32.MaxValue;
        Console.WriteLine("No of test cases" + noOfTestCases);
        for (int i = 0; i < noOfTestCases; i++)
        {

            var noOfElements = Convert.ToInt32(reader.ReadLine());
            //ranks[0] = Int32.MaxValue;
            //ranks[noOfKids + 1] = Int32.MaxValue;
            Console.WriteLine("No of test cases" + noOfElements);
            //var noOfElements = Convert.ToInt32(temp);
            var readLine = reader.ReadLine();
            Console.WriteLine("Read LIne " + readLine);
            var arr = readLine.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
           
            var sumArr = new int[noOfElements];
            Console.WriteLine("Created new  Sum array");
            sumArr[0] = arr[0];
            for (int j = 1; j < noOfElements; j++)
                sumArr[j] = sumArr[j - 1] + arr[j];
            Console.WriteLine("Copied Elements to new array");
            var result = SolveSubProb(sumArr, 0, noOfElements - 1);
            writer.WriteLine();

        }

    }

    static int SolveSubProb( int[] sumArray, int startPos, int endPos)
    {
        Console.WriteLine("Solving sub pro " + startPos);
        if (startPos == endPos)
            return 0;

        //bool canBeSplit = false;
        int? splitPos = null;
        for (int i = startPos; i <= endPos; i++)
        {
            if (sumArray[i] * 2 == sumArray[endPos])
            {
                splitPos = i;
                var spliValue = sumArray[i];
                for (int k = i + 1; k <= endPos; k++)
                    sumArray[k] -= spliValue;
                break;
            }
        }

        if (splitPos.HasValue)
        {
            var firstHalf = 1 + SolveSubProb( sumArray, startPos, splitPos.Value);
            var secondHalf = 1 + SolveSubProb( sumArray, splitPos.Value + 1, endPos);
            return firstHalf > secondHalf ? firstHalf : secondHalf;
        }

        return 0;
    }
}

