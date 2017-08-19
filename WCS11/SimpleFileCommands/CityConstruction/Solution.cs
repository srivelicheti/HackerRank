using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityConstruction
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
            writer = Console.out; // new StreamWriter(Console.OpenStandardOutput());
#endif
            Solve();
#if DEBUG
            Console.WriteLine("Completed");
            Console.ReadLine();
#endif
        }

        static void AddEdge(int u, int v, Dictionary<int, HashSet<int>> city, Dictionary<int, HashSet<int>> inci)
        {
            bool edgeAdded = true;
            if (!city.ContainsKey(u))
                city.Add(u, new HashSet<int>() { v });
            else if (!city[u].Contains(v))
                city[u].Add(v);
            else
                edgeAdded = false;

            if (edgeAdded)
            {
                if (city.ContainsKey(v))
                {
                    foreach (var i in city[v])
                    {
                        AddEdge(u, i, city, inci);
                    }
                }

                if (!inci.ContainsKey(v))
                    inci.Add(v, new HashSet<int>() { u });
                else if (!inci[v].Contains(u))
                    inci[v].Add(u);

                if (inci.ContainsKey(u))
                {
                    foreach (var x in inci[u])
                    {
                        AddEdge(x, v, city, inci);
                    }

                }
            }



        }

        static void Solve()
        {
            var city = new Dictionary<int, HashSet<int>>();
            var inci = new Dictionary<int, HashSet<int>>();

            var t1 = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            var n = t1[0];
            var m = t1[1];
            var t3 = m;

            while (t3-- > 0)
            {
                var t4 = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                var u = t4[0];
                var v = t4[1];
                AddEdge(u, v, city, inci);
            }

            var q = Convert.ToInt32(reader.ReadLine());
            while (q-- > 0)
            {
                var t5 = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                if (t5[0] == 1)
                {
                    var newCity = ++n;
                    var existing = t5[1];
                    if (t5[2] == 0)
                    {
                        AddEdge(existing, newCity, city, inci);
                    }
                    else
                    {
                        AddEdge(newCity, existing, city, inci);
                    }
                }
                else
                {
                    if (city.ContainsKey(t5[1]) && city[t5[1]].Contains(t5[2]))
                    {
                        Console.WriteLine("Yes");
                    }
                    else
                    {
                        Console.WriteLine("No");
                    }
                }
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static bool BFS(Dictionary<int, HashSet<int>> dic, int start, int end)
        {
            bool found = false;
            HashSet<int> visisted = new HashSet<int>();
            var q = new Queue<int>();
            q.Enqueue(start);
            while (q.Count != 0)
            {
                var currentCity = q.Dequeue();
                if (dic.ContainsKey(currentCity))
                {
                    var currentRoads = dic[currentCity];
                    if (currentRoads.Contains(end))
                    {
                        found = true;
                        break;
                    }

                    foreach (var i in currentRoads)
                    {
                        if (!visisted.Contains(i))
                        {
                            visisted.Add(i);
                            q.Enqueue(i);
                        }
                    }
                }
            }


            return found;
        }

        class CompressionData
        {
            public CompressionData(int c)
            {
                City = c;
                ParentTree = new List<int>();
            }
            public int City { get; set; }
            public List<int> ParentTree
            {
                get;
                set;
            }
        }
        private static bool BFSWithPathCompression(Dictionary<int, HashSet<int>> dic, int start, int end)
        {
            bool found = false;

            var q = new Queue<CompressionData>();
            q.Enqueue(new CompressionData(start));
            while (q.Count != 0)
            {
                var currentCity = q.Dequeue();
                if (dic.ContainsKey(currentCity.City))
                {
                    var currentRoads = dic[currentCity.City];
                    if (currentRoads.Contains(end))
                    {
                        found = true;
                        break;
                    }

                    foreach (var i in currentRoads)
                    {
                        q.Enqueue(i);
                    }
                }
            }


            return found;
        }
    }
}
