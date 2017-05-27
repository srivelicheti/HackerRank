using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileCommands
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

        class RefCount
        {
           public int max = -1;
            public SortedSet<int> missing = new SortedSet<int>();
        }

        static void Solve()
        {
            Dictionary<string,RefCount> dic = new Dictionary<string, RefCount>();
            int n = Convert.ToInt32(reader.ReadLine());

            while (n-- > 0)
            {
                var temp = reader.ReadLine().Split(' ');
                if (temp[0] == "crt")
                {
                    CreateBook(temp[1],dic,true);
                }
                else if (temp[0] == "del")
                {
                    DeleteBook(temp[1], dic, true);
                }
                else
                {
                    RenameBook(temp[1], temp[2], dic);
                }

            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static int CreateBook(string s, Dictionary<string, RefCount> dic,bool print)
        {
            if (!dic.ContainsKey(s))
            {
                if(print)
                    printAdd(s,0);
                var refCount = new RefCount();
                refCount.max = 0;
                dic.Add(s,refCount);
                return 0;
            }
            else
            {
                var refCount = dic[s];
                if (refCount.missing.Count == 0)
                {
                    refCount.max = refCount.max + 1;
                    if(print)
                        printAdd(s,refCount.max);
                    return refCount.max;
                }
                else
                {
                    var min = refCount.missing.Min();
                    refCount.missing.Remove(min);
                    if(print)
                        printAdd(s,min);
                    if (min > refCount.max)
                    {
                        refCount.max = min;
                    }
                    return min;

                }
            }
        }


        private static int DeleteBook(string s, Dictionary<string, RefCount> dic,bool print)
        {

            var bookNum = GetBookNumber(s);
            var refCount = dic[bookNum.Item1];

            if (refCount.max == bookNum.Item2)
            {
               
                refCount.max--;
            }
            else
            {
                refCount.missing.Add(bookNum.Item2);
            }                
            if(print)
                printDel(s, 0);

            return bookNum.Item2;
        }


        private static void RenameBook(string s1,string s2 ,Dictionary<string, RefCount> dic)
        {
           var delBNum = DeleteBook(s1,dic,false);
           var crecBNum = CreateBook(s2,dic,false);
            printRen(s1,0,s2,crecBNum);
        }


        private static Tuple<string, int> GetBookNumber(string s)
        {
            if (!s.EndsWith(")"))
            {
                return new Tuple<string, int>(s,0);// 0;
            }
            else
            {
                var i = s.IndexOf("(");
                var temp = s.Substring(i);
                var name = s.Substring(0, i);
               var num = Convert.ToInt32(temp.Substring(1, temp.Length - 2));
                return new Tuple<string, int>(name,num);
            }
        }


        static void printAdd(string s, int i)
        {
            if (i == 0)
                Console.WriteLine("+ " + s);
            else
            {
                Console.WriteLine("+ " + s + "(" + i + ")");
            }
        }

        static void printDel(string s, int i)
        {
            if (i == 0)
                Console.WriteLine("- " + s);
            else
            {
                Console.WriteLine("- " + s + "(" + i + ")");
            }
        }

        static void printRen(string s, int i, string r,int j)
        {
            if (i == 0)
                Console.Write("r " + s);
            else
            {
                Console.Write("r " + s + "(" + i + ")");
            }

            if (j == 0)
                Console.WriteLine(" -> " + r);
            else
            {
                Console.WriteLine(" -> " + r + "(" + j + ")");
            }

        }
    }
}
