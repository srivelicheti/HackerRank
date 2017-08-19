using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RicheRich
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

        static void Solve()
        {

            int n, k;
            
            var temp = reader.ReadLine().Split(new char[] { ' '},StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
            n = temp[0];
            k = temp[1];
            if (n <= k)
            {
                for(int i=0;i<n;i++)
                    writer.Write("9");
                writer.WriteLine();
                return;
            }
            var s = reader.ReadLine();
            var mem = new string[n, n , k + 1];
            
            for (int i = 0; i < n; i++)
            {
                mem[i, i, 0] = s[i].ToString();
                if(k >=1)
                mem[i, i, 1] = "9";
            }
            string res = SolveInternal(s, mem, 0, n - 1, k, n - 1, k);
            if(string.IsNullOrEmpty(res))
                writer.WriteLine("-1");
            else
                writer.WriteLine(res);



            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        static string SolveInternal(string str , string[,,] mem, int s, int e,int k, int n ,int init)
        {
            if (k < 0)
                return string.Empty;
            if (k == 0 && str[s] != str[e])
                return string.Empty;

            if (mem[s, e, k] != null)
                return mem[s, e, k];
            if (e == s + 1)
            {
                if (k == 0 && str[s] != str[e])
                {
                    mem[s, e, k] = string.Empty;
                    return string.Empty;
                }
                else if (k == 0 && str[s] == str[e])
                {
                    mem[s, e, k] = str.Substring(s, 2);
                    return mem[s, e, k];
                }
                else if (k == 1)
                {
                    mem[s, e, k] = Convert.ToInt32(str[s]) > Convert.ToInt32(str[e])
                        ? str[s].ToString() + str[s].ToString()
                        : str[e].ToString() + str[e].ToString();
                    return mem[s, e, k];
                }
                else
                {
                    mem[s, e, k] = "99";
                    return mem[s, e, k];
                }

            }

            if (k >= 2)
            {
                string middle = SolveInternal(str, mem, s + 1, e - 1, k - 2, n, init);
                if (!string.IsNullOrEmpty(middle))
                {
                    mem[s, e, k] = "9" + middle + "9";
                    return mem[s, e, k];
                }
            }
            if (k >= 1)
            {
                if (str[s] == str[e])
                {
                    string middle = SolveInternal(str, mem, s + 1, e - 1, k, n, init);
                    if (!string.IsNullOrEmpty(middle))
                    {
                        mem[s, e, k] = str[s] + middle + str[s];
                        return mem[s, e, k];
                    }
                }
                else
                {
                    string middle = SolveInternal(str, mem, s + 1, e - 1, k-1, n, init);
                    char app = Convert.ToInt32(str[s]) > Convert.ToInt32(str[e])
                        ? str[s]
                        : str[e];

                    if (!string.IsNullOrEmpty(middle))
                    {
                        mem[s, e, k] = app + middle + app;
                        return mem[s, e, k];
                    }
                }
            }
            if (k == 0 && str[s] == str[e])
            {
                string middle = SolveInternal(str, mem, s + 1, e - 1, k, n, init);
                if (!string.IsNullOrEmpty(middle))
                {
                    mem[s, e, k] = str[s] + middle + str[s];
                    return mem[s, e, k];
                }
            }

            mem[s,e,k] = string.Empty;
            return mem[s, e, k];
        }
    }
}
