using System;
using System.IO;

namespace MakeItAnagram
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
            var fArray = new int[26];
            var sArray = new int[26];
            var first = reader.ReadLine();
            var second = reader.ReadLine();

            foreach (var c in first)
            {
                var i = ((int) c) - 97;
                fArray[i] = fArray[i] + 1;
            }

            foreach (var c in second)
            {
                var i = ((int)c) - 97;
                sArray[i] = sArray[i] + 1;
            }

            var toDelete = 0;
            for (int i = 0; i < 26; i++)
            {
                toDelete = toDelete + Math.Abs(fArray[i] - sArray[i]);
            }

            writer.WriteLine(toDelete);

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
