using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingNumbers
{
    class Program
    {
        

        static void Solution1()
        {
            var fLen = Convert.ToInt32(reader.ReadLine());
            var fArr = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToArray();
            var sLen = Convert.ToInt32(reader.ReadLine());
            var sArr = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToArray();
            HashSet<int> final = new HashSet<int>();
            int i = 0, j = 0;
            while (true)
            {
                if (fArr[i] == sArr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    //if(!final.Contains(sArr[j]))
                    final.Add(sArr[j]);
                    j++;
                }
                if (i == fLen)
                {
                    if (j != sLen)
                    {
                        for (int k = j; k < sLen; k++)
                        {
                            final.Add(sArr[k]);
                        }
                    }
                    break;
                }
            }
            Console.Write(string.Join(" ", final));
        }

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
            Solution2();
           
            Console.ReadLine();
        }

        static void Solution2()
        {
            var fLen = Convert.ToInt32(reader.ReadLine());
            var fHashSet = new Dictionary<int, int>(fLen);
            foreach (var i in reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)))
            {
                if (fHashSet.ContainsKey(i))
                    fHashSet[i] = fHashSet[i] + 1;
                else
                {
                    fHashSet.Add(i,1);
                }
            }
            
            var sLen = Convert.ToInt32(reader.ReadLine());
            var sHashSet = new Dictionary<int, int>(sLen);
            foreach (var i in reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)))
            {
                if (sHashSet.ContainsKey(i))
                    sHashSet[i] = sHashSet[i] + 1;
                else
                {
                    sHashSet.Add(i, 1);
                }
            }
            var final = new List<int>();
            foreach (var k in sHashSet.Keys)
            {
                if (!fHashSet.ContainsKey(k) || fHashSet[k] != sHashSet[k])
                {
                    final.Add(k);
                }
            }
            Console.Write(string.Join(" ", final.OrderBy(x => x)));
        }
    }
}
