using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BonnieAndClyde
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
            int n, m, q;
            var temp = reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
            n = temp[0];
            m = temp[1];
            q = temp[2];

            List<int>[] graph = new List<int>[n+1];

            int te = m;

            while (te-- > 0)
            {
                var z = reader.ReadLine();
                var temp2 = z.Split(new char[] { ' '},StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
                if(graph[temp2[0]] == null)
                    graph[temp2[0]] = new List<int>();
                    graph[temp2[0]].Add(temp2[1]);

                if (graph[temp2[1]] == null)
                    graph[temp2[1]] = new List<int>();

                graph[temp2[1]].Add(temp2[0]);
            }

            //HashSet<int> pTemp = new HashSet<int>();
            //FindPath(1, 7, graph, pTemp, new bool[n + 1]);
            //HashSet<int> pTemp2 = new HashSet<int>();
            //FindOtherPath(4, 12, graph, pTemp2, new bool[n + 1],pTemp);
            //return;

            while (q -- > 0)
            {
                SolveOne(graph, n , m);
                writer.Flush();
            }

            
#if DEBUG
            writer.Close();
#endif
        }

        private static void SolveOne(List<int>[] graph, int n , int m)
        {
            

            int u, v, w;
            var temp = reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
            u = temp[0];
            v = temp[1];
            w = temp[2];

            if (graph[w] == null || (graph[w].Count <= 1))
            {
                Console.WriteLine("NO");
                return;
            }

            Dictionary<int,List<HashSet<int>>> dicB = new Dictionary<int, List<HashSet<int>>>();
            Dictionary<int, List<HashSet<int>>> dicC = new Dictionary<int, List<HashSet<int>>>();

            foreach (var src in graph[w])
            {
                if (src == v)
                    continue;
                var path = new HashSet<int>();
                var vis1 = new bool[n + 1];
                vis1[src] = true;
                vis1[w] = true;
                var p1 = FindPath(src, u, graph, path, vis1);
                if (p1 >= 0)
                {
                    dicB.Add(src,new List<HashSet<int>> {path});
                    var exisPathHashSet = dicB[src][0];
                    path = new HashSet<int>();
                    var vis2 = new bool[n + 1];
                    vis2[w] = true;
                    vis2[src] = true;
                    var anotherPath = FindOtherPath(src, u, graph, path, vis2, exisPathHashSet);
                    if (anotherPath >= 0)
                    {
                        dicB[src].Add(path);
                    }
                }
            }

            foreach (var src in graph[w])
            {
                if(src == u)
                    continue;
                var path = new HashSet<int>();
                var vis1 = new bool[n + 1];
                vis1[src] = true;
                vis1[w] = true;
                var p1 = FindPath(src, v, graph, path, vis1);
                if (p1 >= 0)
                {
                    dicC.Add(src, new List<HashSet<int>> { path });
                    var exisPathHashSet = dicC[src][0];
                   
                    path = new HashSet<int>();
                    var vis2 = new bool[n + 1];
                    vis2[src] = true;
                    vis2[w] = true;
                    var anotherPath = FindOtherPath(src, v, graph, path, vis2, exisPathHashSet);
                    if (anotherPath >= 0)
                    {
                        dicB[src].Add(path);

                        // CHeck if any other dest has more than one path to Bonnie then we can stop
                        foreach (var kvp in dicB)
                        {
                            if (kvp.Key != src)
                            {
                                if (kvp.Value != null && kvp.Value.Count == 2)
                                {
                                    Console.WriteLine("YES");
                                    return;
                                }
                            }
                        }

                    }
                }

                
            }

            foreach (var bonPaKvp in dicB)
            {
                var dInd = bonPaKvp.Key;

                if (bonPaKvp.Value != null)
                {
                    foreach (var path in bonPaKvp.Value)
                    {
                        foreach (var clPaKvp in dicC)
                        {
                            if (clPaKvp.Key != dInd && clPaKvp.Value != null)
                            {
                                foreach (var clPath in clPaKvp.Value)
                                {
                                    if (AreDistinct(path, clPath))
                                    {
                                        Console.WriteLine("YES");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

            }

            Console.WriteLine("NO");
        }

        private static bool AreDistinct(HashSet<int> h1, HashSet<int> h2)
        {
            foreach (var i in h1)
            {
                if (h2.Contains(i))
                    return false;
            }
            return true;
        }


        private static int FindPath(int s, int d, List<int>[] graph, HashSet<int> path , bool[] visited)
        {
            if (s == d)
            {
                path.Add(s);
                return s;
            }
            if (graph[s] != null)
            {
                foreach (var i in graph[s])
                {
                    if (!visited[i])
                    {
                        visited[i] = true;
                        var found = FindPath(i, d, graph, path, visited);
                        if (found >= 0)
                        {
                            path.Add(s);
                            return s;
                        }
                    }
                }
            }

            return -1;
        }

        private static int FindOtherPath(int s, int d, List<int>[] graph, HashSet<int> path, bool[] visited, HashSet<int> existingPath )
        {
            if (existingPath.Contains(s))
                return -1;

            if (s == d)
            {
                path.Add(s);
                return s;
            }
            if (graph[s] != null)
            {
                foreach (var i in graph[s])
                {
                    if (!visited[i] && !existingPath.Contains(i))
                    {
                        visited[i] = true;
                        var found = FindOtherPath(i, d, graph, path, visited,existingPath);
                        if (found >= 0)
                        {
                            path.Add(s);
                            return s;
                        }
                    }
                }
            }

            return -1;
        }

    }




}
