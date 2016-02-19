using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SherlockAndArray
{
    class Program
    {
        static void Main(string[] args)
        {
            var noOfTestCases = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < noOfTestCases; i++)
            {
                var length = Convert.ToInt32(Console.ReadLine());
                var arr = Console.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                var fArray = new int[length];
                var sArray = new int[length];
                fArray[0] = arr[0];
                for (int j = 1; j < length; j++)
                    fArray[j] = fArray[j - 1] + arr[j];

                sArray[length - 1] = arr[length - 1];
                for (int j = length-2; j >= 0; j--)
                    sArray[j] = sArray[j + 1] + arr[j];

                bool exists = false;
                for (int j = 0; j < length; j++)
                {
                    if (fArray[j] == sArray[j])
                        exists = true;
                }
                if(exists)
                    Console.WriteLine("YES");
                else
                    Console.WriteLine("NO");
            }
            Console.ReadLine();
        }
    }
}
