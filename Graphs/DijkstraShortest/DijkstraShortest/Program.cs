using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DijkstraShortest
{
    class Program
    {
        protected static TextReader reader;
        protected static TextWriter writer;
        static void Main(string[] args)
        {

#if DEBUG
            reader = new StreamReader("..\\..\\input.txt");
             writer = Console.Out;
            //writer = new StreamWriter("..\\..\\output.txt");
#else
        reader = Console.In;
        writer = new StreamWriter(Console.OpenStandardOutput());
#endif
            Solve();

            //Console.WriteLine("Completed");
            //Console.ReadLine();
        }

        static void Solve()
        {
            var noOfTestCases = Convert.ToInt32(reader.ReadLine());
            for (int x = 0; x < noOfTestCases; x++)
            {
                var temp = reader.ReadLine().Split(' ').Select(z => Convert.ToInt32(z)).ToArray();
                var nodes = temp[0];
                var edges = temp[1];
                var graph = new Dictionary<int, Dictionary<int, int>>();
                for (int i = 0; i < nodes; i++)
                {
                    graph[i] = new Dictionary<int, int>();
                }
                for (int y = 0; y < edges; y++)
                {
                    var edge = reader.ReadLine().Split(' ').Select(z => Convert.ToInt32(z)).ToArray();
                    var sEdge = edge[0] - 1;
                    var tEdge = edge[1] - 1;
                    if (!graph[sEdge].ContainsKey(tEdge))
                        graph[sEdge].Add(tEdge, edge[2]);
                    else
                    {
                        if (graph[sEdge][tEdge] > edge[2])
                            graph[sEdge][tEdge] = edge[2];
                    }

                    if (!graph[tEdge].ContainsKey(sEdge))
                        graph[tEdge].Add(sEdge, edge[2]);
                    else
                    {
                        if (graph[tEdge][sEdge] > edge[2])
                            graph[tEdge][sEdge] = edge[2];
                    }

                }
                var startNode = Convert.ToInt32(reader.ReadLine()) - 1;
                var distance = new int[nodes];
                for (int i = 0; i < distance.Length; i++)
                {
                    distance[i] = -1;
                }
                var q = new Queue<int>();
                q.Enqueue(startNode);
                distance[startNode] = 0;
                while (q.Count > 0)
                {

                    var currentNode = q.Dequeue();
                    var currentNodeEdges = graph[currentNode];
                    foreach (var kvp in currentNodeEdges)
                    {
                        var newDist = distance[currentNode] + kvp.Value;
                        if (distance[kvp.Key] == -1)
                        {
                            q.Enqueue(kvp.Key);
                            distance[kvp.Key] = newDist;
                        }
                        else
                        {
                            if (newDist < distance[kvp.Key])
                            {
                                distance[kvp.Key] = newDist;
                                q.Enqueue(kvp.Key);
                            }

                        }
                    }
                }
                
                for (int i = 0; i < nodes; i++)
                {
                    if (i == startNode)
                        continue;
                    else
                    {
                        if (i == nodes)
                            writer.Write(distance[i] == 0 ? -1 : distance[i]);
                        else
                        {
                            writer.Write((distance[i] == 0 ? -1 : distance[i]) + " ");
                        }
                    }
                }

                writer.WriteLine("");
                writer.Flush();

            }
        }

    }
}
