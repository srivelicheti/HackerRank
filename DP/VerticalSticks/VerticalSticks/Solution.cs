using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VerticalSticks
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
            //Console.WriteLine("Completed");
            //Console.ReadLine();
#endif
            //Console.ReadLine();
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
            var n = Convert.ToInt32(reader.ReadLine());
            var arr = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

            var perms = GeneratePermutation(arr, 0, arr.Length - 1);
            if (perms.Count > 0)
            {
                int sum = 0;
                foreach (var perm in perms)
                {
                    sum += SolveOneSub(perm.ToArray());
                    //Console.WriteLine($"{Join(", ", perm.Select(x => x.ToString()))} : {}");
                }

                decimal d = perms.Count;
                writer.WriteLine(Math.Round(sum / d, 2).ToString("0.00"));

            }


        }

        private static int SolveOneSub(int[] arr)
        {

            Stack<Tuple<int, int>> s = new Stack<Tuple<int, int>>();

            s.Push(new Tuple<int, int>(arr[0], 0));
            int sum = 1;

            for (int i = 1; i < arr.Length; i++)
            {
                //int index = -1;
                while (s.Count > 0)
                {
                    var peek = s.Peek();
                    if (peek.Item1 >= arr[i])
                        break;
                    s.Pop();
                }
                var item = s.Count > 0 ? s.Peek() : null;
                if (item == null)
                    sum += (i + 1);
                else
                    sum += (i - item.Item2);
                s.Push(new Tuple<int, int>(arr[i], i));
            }

            return sum;
            //int max = arr[0];
            //int maxPos = 0;
            //var newArr = new int[arr.Length];
            //newArr[0] = 1;
            //for (int i = 1; i < arr.Length; i++)
            //{
            //    if (arr[i] > max)
            //    {
            //        newArr[i] = i + 1;
            //        max = arr[i];
            //        maxPos = i;
            //    }
            //    else
            //    {
            //        newArr[i] = i - maxPos;
            //    }
            //}

            //return newArr.Sum();

            //throw new NotImplementedException();
        }


        private static List<List<int>> GeneratePermutation(int[] p, int start, int end)
        {
            if (p.Length == 0 ||  end < start || start < 0 || end < 0)
                return new List<List<int>>();
            if (start == end)
                return new List<List<int>>(1) { new List<int>() { p[start] } };

            int x = p[end];
            --end;
            var oneLessPerm = GeneratePermutation(p, start,end);
            var ret = new List<List<int>>();

            foreach (var perm in oneLessPerm)
            {
                int currPos = 0;
                while (currPos <= perm.Count)
                {
                    ret.Add(CloneAndInsertAt(perm, x, currPos));
                    currPos++;
                }
            }
            return ret;
        }

        private static List<int> CloneAndInsertAt(List<int> p, int x, int pos)
        {
            var toAdd = new List<int>(p.Count + 1);
            for (int i = 0; i < p.Count; i++)
            {
                if (i == pos) toAdd.Add(x);
                toAdd.Add(p[i]);
            }
            if (pos == p.Count)
                toAdd.Add(x);
            return toAdd;
        }

    }
}
