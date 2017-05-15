using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbreviation
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

            int n = Convert.ToInt32(reader.ReadLine());

            while (n-- > 0)
            {
               writer.WriteLine( SolveOneProblem());
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static string SolveOneProblem()
        {
            var s1 = reader.ReadLine();
            var s2 = reader.ReadLine();
            if (s2.Length > s1.Length)
                return "NO";

            var x = s1.Length + 1;
            var y = s2.Length + 1;
            var arr = new bool[x,y];
            
            for (int i = 0; i < s1.Length+1; i++)
            {
                for (int j = 0; j < s2.Length+1; j++)
                {
                    if (j == 0 && i == 0)
                        arr[i, j] = true;
                    else if (j == 0)
                        arr[i, j] = !char.IsUpper(s1[i-1]) && arr[i-1,j];
                    else if (i == 0 && j != 0)
                        arr[i, j] = false;
                    else
                    {
                        if (char.ToUpper(s1[i - 1]) == s2[j - 1] && arr[i - 1, j - 1]) {
                            arr[i, j] = true;
                        }
                        else{
                            arr[i, j] = char.IsLower(s1[i-1]) && arr[i - 1, j]; //Delete ith char and compare it to the second string
                        }
                    }
                }
            }

            return arr[x - 1,y - 1] ? "YES" : "NO";


        }
    }
}
