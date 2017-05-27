using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace BiggerIsGreater
{
    class Solution
    {
        protected static TextReader reader;
        protected static TextWriter writer;

        static void Main(string[] args)
        {
#if DEBUG
            reader = new StreamReader("..\\..\\input.txt");
            writer = new StreamWriter("..\\..\\output.txt"); //Console.Out;
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
            var noOfTestCases = int.Parse(reader.ReadLine());
            for (int i = 0; i < noOfTestCases; i++)
            {
                var inputStr = reader.ReadLine();
                var input = inputStr.ToCharArray();
                bool foundGreaterString = false;
                var first = (int)input[0];

                int tempNexHigher = int.MaxValue;
                int tempNextHigherIndex = -1;

                int lastChar = input[input.Length - 1];
                if (lastChar > first)
                {
                    tempNexHigher = lastChar;
                    tempNextHigherIndex = input.Length - 1;
                }
                for (int j = input.Length - 2; j >= 0; j--)
                {
                    var curretnChar = (int)input[j];
                    if (curretnChar > first && curretnChar < tempNexHigher)
                    {
                        tempNexHigher = curretnChar;
                        tempNextHigherIndex = j;
                    }
                    if (lastChar > curretnChar)
                    {
                        input[input.Length - 1] = input[j];
                        input[j] = (char)lastChar;
                        Array.Sort(input, j + 1, (input.Length - j - 1));
                        foundGreaterString = true;
                        break;
                    }
                }

                #region old
                //for (int j = input.Length -1 ; j > 0; j--)
                //{
                //    var curretnChar = (int) input[j];
                //    var prevChar = (int) input[j - 1];
                //    if (curretnChar > first && curretnChar < tempNexHigher)
                //    {
                //        tempNexHigher = curretnChar;
                //        tempNextHigherIndex = j;
                //    }

                //    if (curretnChar > prevChar)
                //    {
                //        foundGreaterString = true;
                //        if (j == 1)
                //        {
                //            if (tempNexHigher >= curretnChar)
                //            {
                //                input[j - 1] = (char) curretnChar;
                //                input[j] = (char) prevChar;
                //                Array.Sort(input, j, (input.Length - j - 1));
                //            }
                //            else
                //            {
                //                input[0] = input[tempNextHigherIndex];
                //                input[tempNextHigherIndex] = (char) prevChar;
                //                Array.Sort(input, 1, input.Length - 1);
                //            }
                //        }
                //        else
                //        {
                //            input[j - 1] = (char)curretnChar;
                //            input[j] = (char)prevChar;
                //            Array.Sort(input, j, (input.Length - j));
                //        }
                //        break;
                //    }
                //}
#endregion

                if (!foundGreaterString && tempNextHigherIndex != -1)
                {
                    foundGreaterString = true;
                    input[0] = input[tempNextHigherIndex];
                    input[tempNextHigherIndex] = (char)first;
                    Array.Sort(input, 1, input.Length - 1);
                }

                if (foundGreaterString)
                {
                    foreach (var c in input)
                    {
                        writer.Write(c);
                    }
                    writer.WriteLine("");
                }
                //else if (tempNextHigherIndex != -1)
                //{
                //    input[0] = input[tempNextHigherIndex];
                //    input[tempNextHigherIndex] = (char) first;
                //    Array.Sort(input, 1, input.Length - 1);
                //}
                else
                {
                    writer.WriteLine("no answer");
                }
            }

            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }
    }
}
