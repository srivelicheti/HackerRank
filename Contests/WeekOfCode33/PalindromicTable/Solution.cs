using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace PalindromicTable
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
            int m = temp[0];
            int n = temp[1];

            var inp = new int[m][];

            for (int i = 0; i < m; i++)
            {
                inp[i] = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            }

            var numCache = new Numbers[m, n];

            numCache[0, 0] = new Numbers(inp[0][0]);

            for (int j = 1; j < n; j++)
                numCache[0, j] = numCache[0, j - 1].Add(inp[0][j]);

            for (int i = 1; i < m; i++)
                numCache[i, 0] = numCache[i - 1, 0].Add(inp[i][0]);

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    numCache[i, j] = ((numCache[i, j - 1] + numCache[i - 1, j]) - numCache[i - 1, j - 1]).Add(inp[i][j]);
                }
            }
            int maxLen = 0;
            int msi = -1, msj = -1, mei = -1, mej = -1;

            for (int ei = 0; ei < m; ei++)
            {
                for (int ej = 0; ej < n; ej++)
                {
                    for (int si = 0; si <= ei; si++)
                    {
                        if (area(si, 0, ei, ej) < maxLen)
                            continue;
                        for (int sj = 0; sj <= ej; sj++)
                        {
                            if (area(si, sj, ei, ej) < maxLen)
                                continue;
                            int cMaxLen = CalcMaxLen(numCache, si, sj, ei, ej);
                            if (cMaxLen > maxLen)
                            {
                                maxLen = cMaxLen;
                                msi = si;
                                msj = sj;
                                mei = ei;
                                mej = ej;
                            }
                        }
                    }
                }
            }
            writer.WriteLine(maxLen);
            writer.WriteLine(msi + " " + msj + " " + mei + " " + mej);


            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        private static int area(int si, int sj, int ei, int ej)
        {
            return ((ei - si) + 1) * (ej - sj + 1);
        }

        private static int CalcMaxLen(Numbers[,] numCache, int si, int sj, int ei, int ej)
        {
            if (si == ei && sj == ej)
                return 1;

            var total = numCache[ei, ej];

            int toDelj = sj - 1;
            if (toDelj >= 0)
                total = total - numCache[ei, toDelj];

            int toDeli = si - 1;
            if (toDeli >= 0)
                total = total - numCache[toDeli, ej];

            return total.PalinMaxLen();
        }
    }

    class Numbers
    {
        public Numbers()
        {
        }

        public Numbers(int n)
        {
            if (n == 0)
                Zero = 1;

            if (n == 1)
                One = 1;

            if (n == 2)
                Two = 1;


            if (n == 3)
                Three = 1;


            if (n == 4)
                Four = 1;


            if (n == 5)
                Five = 1;


            if (n == 6)
                Six = 1;


            if (n == 7)
                Seven = 1;


            if (n == 8)
                Eight = 1;


            if (n == 9)
                Nine = 1;
        }
        public Numbers(int z, int o, int t, int thr, int f, int fiv, int s, int seve, int ei, int ni)
        {
            Zero = z;
            One = o;
            Two = t;
            Three = thr;
            Four = f;
            Five = fiv;
            Six = s;
            Seven = seve;
            Eight = ei;
            Nine = ni;
        }
        public int Zero { get; set; }
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
        public int Six { get; set; }
        public int Seven { get; set; }
        public int Eight { get; set; }
        public int Nine { get; set; }


        public Numbers Add(int n)
        {
            if (n == 0)
                return AddZero();

            else if (n == 1)
                return AddONe();

            else if (n == 2)
                return AddTwo();


            else if (n == 3)
                return AddThree();


            else if (n == 4)
                return AddFour();


            else if (n == 5)
                return AddFive();


            else if (n == 6)
                return AddSix();


            else if (n == 7)
                return AddSeven();


            else if (n == 8)
                return AddEight();


            else if (n == 9)
                return AddNine();
            else
                throw new Exception("INvalid number");
        }

        public Numbers AddZero()
        {
            return new Numbers(Zero + 1, One, Two, Three, Four, Five, Six, Seven, Eight, Nine);
        }

        public Numbers AddONe()
        {
            return new Numbers(Zero, One + 1, Two, Three, Four, Five, Six, Seven, Eight, Nine);
        }

        public Numbers AddTwo()
        {
            return new Numbers(Zero, One, Two + 1, Three, Four, Five, Six, Seven, Eight, Nine);
        }

        public Numbers AddThree()
        {
            return new Numbers(Zero, One, Two, Three + 1, Four, Five, Six, Seven, Eight, Nine);
        }

        public Numbers AddFour()
        {
            return new Numbers(Zero, One, Two, Three, Four + 1, Five, Six, Seven, Eight, Nine);
        }
        public Numbers AddFive()
        {
            return new Numbers(Zero, One, Two, Three, Four, Five + 1, Six, Seven, Eight, Nine);
        }
        public Numbers AddSix()
        {
            return new Numbers(Zero, One, Two, Three, Four, Five, Six + 1, Seven, Eight, Nine);
        }
        public Numbers AddSeven()
        {
            return new Numbers(Zero, One, Two, Three, Four, Five, Six, Seven + 1, Eight, Nine);
        }
        public Numbers AddEight()
        {
            return new Numbers(Zero, One, Two, Three, Four, Five, Six, Seven, Eight + 1, Nine);
        }
        public Numbers AddNine()
        {
            return new Numbers(Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine + 1);
        }

        public static Numbers operator +(Numbers n1, Numbers n2)
        {
            return new Numbers(n1.Zero + n2.Zero,
                n1.One + n2.One,
                n1.Two + n2.Two,
                n1.Three + n2.Three,
                n1.Four + n2.Four,
                n1.Five + n2.Five,
                n1.Six + n2.Six,
                n1.Seven + n2.Seven,
                n1.Eight + n2.Eight,
                n1.Nine + n2.Nine);
        }

        public static Numbers operator -(Numbers n1, Numbers n2)
        {
            return new Numbers(n1.Zero - n2.Zero,
               Math.Abs(n1.One - n2.One),
               Math.Abs(n1.Two - n2.Two),
                Math.Abs(n1.Three - n2.Three),
                Math.Abs(n1.Four - n2.Four),
                Math.Abs(n1.Five - n2.Five),
                Math.Abs(n1.Six - n2.Six),
                Math.Abs(n1.Seven - n2.Seven),
                Math.Abs(n1.Eight - n2.Eight),
                Math.Abs(n1.Nine - n2.Nine));
        }

        public int PalinMaxLen()
        {
            int palinLen = 0;
            bool oddFound = false;

            if (One % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += One;

            if (Two % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Two;

            if (Three % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Three;

            if (Four % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Four;

            if (Five % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Five;

            if (Six % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Six;

            if (Seven % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Seven;

            if (Eight % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Eight;

            if (Nine % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Nine;

            if (Zero % 2 == 1)
            {
                if (oddFound)
                    return -1;
                oddFound = true;
            }
            palinLen += Zero;

            if (palinLen == Zero || palinLen == (Zero - 1))
            {
                return -1;
            }
            return palinLen;

        }

        public override string ToString()
        {
            return $"0:{Zero} 1:{One} 2:{Two} 3:{Three} 4:{Four} 5:{Five} 6:{Six} 7:{Seven} 8:{Eight} 9:{Nine}";
        }
    }
}
