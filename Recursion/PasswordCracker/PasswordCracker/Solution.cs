using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCracker
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
                SolveOne();
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static void SolveOne()
        {
            int n = Convert.ToInt32(reader.ReadLine());
            List<string> _dict = reader.ReadLine().Split(' ').ToList();
            Dictionary<string, List<string>> _cache = new Dictionary<string, List<string>>();
            var word = reader.ReadLine();

            List<string> final = SolveRec(word, 0, word.Length - 1, _cache, _dict);
            if(final.Count > 0)
                writer.WriteLine(string.Join(" ",final));
            else
            {
                writer.WriteLine("WRONG PASSWORD");
            }

        }

        private static List<string> SolveRec(string word, int i, int j, Dictionary<string, List<string>> cache, List<string> dict)
        {
            if (j < i || i < 0 || j < 0)
                return new List<string>();
            if (cache.ContainsKey($"{i}_{j}"))
                return cache[$"{i}_{j}"];
            if (dict.Contains(word.Substring(i, j - i +1)))
            {
                var l = new List<string> { word.Substring(i, j - i +1) };
                cache.Add($"{i}_{j}", l);
                return l;
            }


            var res = new List<string>();
            int start = j;
            while (start >= i)
            {
                var w = word.Substring(start , j - start + 1);
                if (dict.Contains(w))
                {
                    var currWord = w;
                    var restOfString = SolveRec(word, i, start- 1, cache, dict);
                    if (restOfString.Count > 0)
                    {
                       restOfString.Add(currWord);
                        res = restOfString;
                        break;
                    }
                }
                start--;
            }
            cache.Add($"{i}_{j}",res);
            return res;

        }
    }
}
