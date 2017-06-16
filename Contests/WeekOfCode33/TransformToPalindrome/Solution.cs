using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformToPalindrome
{

    class UnionFind
    {
        private int _count;
        private int[] _parent;
        private int[] _size;
        
        public UnionFind(int n)
        {
            _parent = new int[n+1];
            _size = new int[n + 1];
            _count = n+1;
            for (int i = 0; i < n + 1; i++)
            {
                _parent[i] = i;
                _size[i] = 0;
            }
        }

        public int find(int p)
        {

            while (p != _parent[p])
            {
                _parent[p] = _parent[_parent[p]];
                p = _parent[p];
            }

            return p;
        }

        public bool connected(int p, int q)
        {
            return find(p) == find(q);
        }

        public void union(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);

            if (_size[rootP] < _size[rootQ])
                _parent[rootP] = rootQ;
            else if (_size[rootP] > _size[rootQ])
                _parent[rootQ] = rootP;
            else
            {
                _parent[rootQ] = rootP;
                _size[rootP]++;
            }
            _count--;
        }
    }

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

            
            var uf = new UnionFind(n);
            while (k-- > 0)
            {
                var temp2 = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                int e1 = temp2[0];
                int e2 = temp2[1];

                uf.union(e1,e2);

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
                            if (AreEqualUF(s[i], s[j], uf, n))
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


        static bool AreEqualUF(int i1, int i2, UnionFind g, int n)
        {
            if (i1 == i2)
                return true;

            return g.connected(i1, i2);
        }
    }

    
}
