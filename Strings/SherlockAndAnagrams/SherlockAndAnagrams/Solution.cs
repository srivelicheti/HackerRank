using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SherlockAndAnagrams
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
            var numOfTestCases = Convert.ToInt32(reader.ReadLine());
            for (int i = 0; i < numOfTestCases; i++)
            {
                Solve();
            }

#if DEBUG
            Console.WriteLine("Completed");
            Console.ReadLine();
#endif
        }

        private static Dictionary<char, List<int>> indexes;
        static void Solve()
        {
            var str = reader.ReadLine();
            indexes = new Dictionary<char, List<int>>();
            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (indexes.ContainsKey(c))
                    indexes[c].Add(i);
                else
                    indexes[c] = new List<int>() { i };
            }
            writer.WriteLine(SolveSubProblems(str,0,str.Length -1 ));
            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        static int SolveSubProblems(string s, int start, int end)
        {
            // if (indexes[c].Count(x => x >= start && x <= end) <= 1)
            int numOfPairs = 0;
            var visited = new List<char>();
            for (int i = start; i <= end; i++)
            {
                var c = s[i];
                if (visited.Contains(c))
                    continue;
                var tempCount = indexes[c].Count(x => x >= start && x <= end);
                if (tempCount <= 1)
                    continue;
                
                numOfPairs = numOfPairs + ((tempCount * tempCount - 1) / 2);
                var curIndexes = indexes[c];
                for (int j = 0; j < curIndexes.Count - 1; j++)
                {
                    var t2 = curIndexes[j];
                    if(t2 <start || t2 > end)
                        continue;
                    for (int z = j + 1; z < curIndexes.Count; z++)
                    {
                        var t1 = curIndexes[z];
                        if(t1 < start || t1 > end)
                            continue;
                        if (t1 - t2 == 1)
                            continue;
                        else if (t1 - t2 == 2)
                        {
                            if (s[t2 + 1] != c)
                                numOfPairs = numOfPairs + 1;
                        }
                        else
                        {
                            numOfPairs++;
                            numOfPairs = numOfPairs + SolveSubProblems2(s, t2 + 1, t1 - 1);
                        }
                    }
                }

                visited.Add(c);

            }
            return numOfPairs;
        }


        static int SolveSubProblems2(string s, int start, int end)
        {

           
            var indices = new Dictionary<char, List<int>>();
            for (int i = start; i <= end; i++)
            {
                var c = s[i];
                if (indices.ContainsKey(c))
                    indices[c].Add(i);
                else
                    indices[c] = new List<int>() { i };
            }

            int numOfPairs = 0;
            var visited = new List<char>();
            for (int i = start; i <= end; i++)
            {
                var c = s[i];
                if(visited.Contains(c))
                    continue;
                if (indices[c].Count() <= 1)
                    continue;
                var tempCount = indices[c].Count;
                numOfPairs = numOfPairs + ((tempCount*tempCount - 1)/2);
                var curIndexes = indices[c];
                for (int j = 0; j < curIndexes.Count - 1; j++)
                {
                    for (int z = j + 1; z < curIndexes.Count; z++)
                    {
                        var t1 = curIndexes[z];
                        var t2 = curIndexes[j];
                        if (t1 - t2 == 1)
                            continue;
                        else if (t1 - t2 == 2)
                        {
                            if(s[t2 +1] != c)
                                numOfPairs = numOfPairs + 1;
                        }
                        else
                        {
                            numOfPairs++;
                            numOfPairs = numOfPairs + SolveSubProblems2(s, t2 + 1, t1 - 1);
                        }
                    }
                }

                visited.Add(c);

            }
            return numOfPairs;
        }
    }
}
