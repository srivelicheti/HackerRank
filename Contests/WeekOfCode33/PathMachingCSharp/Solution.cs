using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathMachingCSharp
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
            var temp = reader.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            int n = temp[0];
            int q = temp[1];

            string s = reader.ReadLine();
            string p = reader.ReadLine();

            var graph = new List<int>[n+1];
            
            for (int i = 0; i < n+1; i++)
            {
                graph[i] = new List<int>(10);
            }
            var t = n-1;
            while (t-- > 0)
            {
                var temp3 = reader.ReadLine();
                var temp2 = temp3.Split(' ').Select(Int32.Parse).ToArray();
                int st = temp2[0];
                int e = temp2[1];
                graph[st].Add(e);
                graph[e].Add(st);

            }

            var pLen = p.Length;
            var lps = new int[p.Length];
            ComputeLPSArray(p, lps,pLen);

            while (q-- > 0)
            {
                SolveOne(graph, s, p, n, pLen, lps);
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static void SolveOne(List<int>[] graph, string s, string p, int n, int pLength, int[] lps)
        {
            int v, u;
            var temp = reader.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            v = temp[0];
            u = temp[1];
            bool[] visisted = new bool[n + 1];

            int count = 0;
            visisted[u] = true;
            int indexToMatch = 0;
            var res = CountOccurances(graph, s, p, ref pLength, u, ref v, visisted, ref indexToMatch, ref count, lps);
            if (res && p[indexToMatch] == s[u - 1])
            {
                indexToMatch++;
                if (indexToMatch == pLength)
                {
                    count++; indexToMatch = lps[indexToMatch - 1];
                }

            }
           writer.WriteLine(count);
        }

        private static bool CountOccurances(List<int>[] graph, string s, string p, ref int pLength, int u,ref int v, bool[] visited, ref int indexToMatch, ref int count, int[] lps)
        {
            if (u == v)
            {
                return true;
            }
            bool res = false;
            foreach (var c in graph[u])
            {
                if (!visited[c])
                {
                    visited[c] = true;
                    res = CountOccurances(graph, s, p,ref pLength, c,ref v, visited, ref indexToMatch, ref count, lps);
                    if (res)
                    {
                        if (p[indexToMatch] == s[c - 1])
                        {
                            indexToMatch++;
                            if (indexToMatch == pLength)
                            {
                                count++; indexToMatch = lps[indexToMatch - 1];
                            }
                        }
                        else
                        {
                            indexToMatch = indexToMatch == 0 ? 0 : lps[indexToMatch - 1];
                            while (indexToMatch >= 0)
                            {
                                if (p[indexToMatch] == s[c - 1])
                                {
                                    indexToMatch++;
                                    if (indexToMatch == pLength)
                                    {
                                        count++; indexToMatch = lps[indexToMatch - 1];
                                    }
                                    break;
                                }
                                if (indexToMatch == 0)
                                    break;
                                indexToMatch = lps[indexToMatch - 1];
                            }
                        }
                        break;
                    }
                }
            }

            return res;
        }

        private static void ComputeLPSArray(string pat, int[] lps, int pLen)
        {
            int len = 0;

            lps[0] = 0; // lps[0] is always 0

            // the loop calculates lps[i] for i = 1 to M-1
            int i = 1;
            while (i < pLen)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else // (pat[i] != pat[len])
                {
                    // This is tricky. Consider the example.
                    // AAACAAAA and i = 7. The idea is similar 
                    // to search step.
                    if (len != 0)
                    {
                        len = lps[len - 1];

                        // Also, note that we do not increment
                        // i here
                    }
                    else // if (len == 0)
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }
    }
}
