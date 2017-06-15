using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformToPalindrome
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
            
            var temp = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            int n = temp[0];
            int k = temp[1];
            int m = temp[2];

            var equalNoCache = new HashSet<int>[n+1];

            var g = new List<int>[n+1];

            while (k-- > 0)
            {
                var temp2 = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                int e1 = temp2[0];
                int e2 = temp2[1];

                if(g[e1] == null)
                    g[e1] = new List<int>();
                if(g[e2] == null)
                    g[e2] = new List<int>();

                g[e1].Add(e2);
                g[e2].Add(e1);

            }
            var s = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            var mem = new int[m, m];
            int maxLen = 1;

            for (int z = 0; z < m; z++)
            {
                for (int i = 0, j = i + z; j < m; i++,j++)
                {
                        if (i == j)
                            mem[i, j] = 1;
                        else
                        {
                            if (AreEqualDfs(s[i], s[j], g, n))
                            {
                                mem[i, j] = mem[i + 1, j - 1] + 2;
                            }
                            else
                            {
                                mem[i, j] = Math.Max(mem[i, j - 1], mem[i + 1, j]);
                            }
                            maxLen = Math.Max(maxLen, mem[i, j]);
                        }
                }
            }

            
            writer.WriteLine(maxLen);


                writer.Flush();

            
#if DEBUG
            writer.Close();
#endif
        }

        static bool AreEqual(int i1, int i2, List<int>[] g, int n)
        {
            if (i1 == i2)
                return true;

            var visited = new bool[n+1];

            visited[i1] = true;

            Queue<int> q = new Queue<int>();
            q.Enqueue(i1);

            while (q.Count > 0)
            {
                int current = q.Dequeue();

                if (g[current] != null)
                {
                    foreach (var child in g[current])
                    {
                        if(visited[child])
                            continue;
                        if (child == i2)
                            return true;

                        visited[child] = true;
                        q.Enqueue(child);

                    }
                }

            }

            return false;
        }

        static bool AreEqualDfs(int i1, int i2, List<int>[] g, int n)
        {
            if (i1 == i2)
                return true;

            var visited = new bool[n + 1];

            visited[i1] = true;

            var q = new Stack<int>();
            q.Push(i1);

            while (q.Count > 0)
            {
                int current = q.Pop();

                if (g[current] != null)
                {
                    foreach (var child in g[current])
                    {
                        if (visited[child])
                            continue;
                        if (child == i2)
                            return true;

                        visited[child] = true;
                        q.Push(child);

                    }
                }

            }

            return false;
        }
    }

    
}
