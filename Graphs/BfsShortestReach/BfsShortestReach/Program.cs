using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BfsShortestReach
{
    //https://www.hackerrank.com/challenges/bfsshortreach
    class Program
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
                var graph = new int[nodes, nodes];
                var edges = temp[1];
                for (int y = 0; y < edges; y++)
                {
                    var edge = reader.ReadLine().Split(' ').Select(z => Convert.ToInt32(z) - 1).ToArray();
                    graph[edge[0], edge[1]] = 1;
                    graph[edge[1], edge[0]] = 1;
                }

                var start = Convert.ToInt32(reader.ReadLine()) - 1;
                int[] p = new int[nodes];
                Queue<int> s = new Queue<int>();
                int[] dist = new int[nodes];
                for (int i = 0; i < dist.Length; i++)
                {
                    dist[i] = -1;
                }
                dist[start] = 0;
                
                 s.Enqueue(start);
                while (s.Count() != 0)
                {
                    var current = s.Dequeue();
                    if (p[current] == 0)
                    {
                        p[current] = 1;
                        for (var k  = 0; k < nodes; k++)
                        {
                            if (graph[current, k] != 0 && dist[k] == -1)
                            {
                                s.Enqueue(k);
                                dist[k] = dist[current] + 6;
                            }
                        }
                    }
                }
                for (int i = 0; i < nodes; i++)
                {
                    if(i == start)
                        continue;
                    else
                    {
                        if (i == nodes)
                            writer.Write(dist[i]);
                        else
                        {
                            writer.Write(dist[i]+ " ");
                        }
                    }
                }

                writer.WriteLine("");
                writer.Flush();
            }
        }
    }
}
