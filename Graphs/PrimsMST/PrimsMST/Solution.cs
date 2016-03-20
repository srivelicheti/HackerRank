using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace PrimsMST
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
            var lens = reader.ReadLine().Split(' ').Select(x=> Convert.ToInt32(x)).ToArray();
            var nodes = lens[0];
            var edges = lens[1];
            var al = new List<Dictionary<int, int>>(nodes);
            for (int i = 0; i < nodes; i++)
            {
                al.Add(new Dictionary<int, int>());
            }
            for (int i = 0; i < edges; i++)
            {
                var lineParts = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                var s = lineParts[0] -1;
                var e = lineParts[1] -1;
                var w = lineParts[2];
                var dic = al[s];
                if (dic.ContainsKey(e))
                {
                    var existingWeigh = dic[e];
                    if (w < existingWeigh)
                        dic[e] = w;
                }
                else
                    dic.Add(e, w);

                 dic = al[e];
                if (dic.ContainsKey(s))
                {
                    var existingWeigh = dic[s];
                    if (w < existingWeigh)
                        dic[s] = w;
                }
                else
                    dic.Add(s, w);

            }

            var start = Convert.ToInt32(reader.ReadLine()) - 1 ;

            Dictionary<int, int> consumedEdges = new Dictionary<int, int>();
            consumedEdges.Add(start, 0);
            var nextEdge = GetNextEdgeToConsume(al, consumedEdges);
            int totalWeight = 0;
            while (nextEdge != null)
            {
                totalWeight = totalWeight + nextEdge.Item3;
                consumedEdges.Add(nextEdge.Item2, nextEdge.Item3);
                nextEdge = GetNextEdgeToConsume(al, consumedEdges);
            }
            writer.WriteLine(totalWeight);

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        static Tuple<int,int,int> GetNextEdgeToConsume(List<Dictionary<int, int>> nodes, Dictionary<int,int> consumedEdges)
        {
            var toAddWeight = int.MaxValue;
            Int32 toAddStart = -1;
            Int32 toAddEnd = -1;
            foreach (var key in consumedEdges.Keys)
            {
                var d = nodes[key];
                foreach (var kvp in d.Where(x => !consumedEdges.ContainsKey(x.Key)))
                {
                    if (kvp.Value < toAddWeight)
                    {
                        toAddWeight = kvp.Value;
                        toAddStart = key;
                        toAddEnd = kvp.Key;
                    }
                }
            }
            if (toAddStart == -1)
                return null;
            else
                return new Tuple<int, int, int>(toAddStart, toAddEnd, toAddWeight);
        }
    }
}
