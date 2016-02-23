using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinChange
{
    class Solution
    {
        protected static TextReader reader;
        protected static TextWriter writer;
        static Dictionary<int,int> _cache = new Dictionary<int, int>();

        static Dictionary<int, List<string>> _secCache = new Dictionary<int, List<string>>();
        private static int[] _coins;
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
        }

        static void Solve()
        {
            _cache.Add(0,0);
            var amount = Convert.ToInt32(reader.ReadLine().Split(' ')[0]);

            _coins = reader.ReadLine().Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            foreach (var c in _coins)
            {
                //_cache.Add(c,1);
            }
            writer.WriteLine(GetNumberOfDenominations(amount));
            writer.Flush();
#if DEBUG
            writer.Close();
#endif
        }

        static int GetNumberOfDenominations(int amount)
        {
            if (_cache.ContainsKey(amount))
                return _cache[amount];
            else
            {
                if (_coins.All(x => x > amount))
                {
                    _cache[amount] = 0;
                    return 0;
                }
                else
                {
                    var total = 0;
                    List<string> currentCoins = new List<string>();
                    HashSet<int> _considered = new HashSet<int>();
                    int maxFigured = 0;
                    foreach (var coin in _coins)
                    {
                        if (!_considered.Contains(coin))
                        {
                            if (coin == amount)
                            {
                                currentCoins.Add(coin.ToString());
                                total++;
                            }
                            else if (coin <= amount)
                            {
                                //if ()
                                {
                                    var toFigure = amount - coin;
                                    var den = GetNumberOfDenominations(toFigure);
                                    _considered.Add(toFigure);
                                    if (den > 0)
                                    {
                                        total = total + den;
                                        foreach (var s in _secCache[toFigure])
                                        {
                                            var f = ( s + coin.ToString()).
                                        }
                                    }
                                }
                            }
                        }
                    }
                    _cache[amount] = total;
                    return total;
                }
            }
        }
    }
}
