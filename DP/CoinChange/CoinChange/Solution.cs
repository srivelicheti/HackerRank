using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinChange
{

    public class CoinCollection
    {
        private IDictionary<int, int> _collection = new Dictionary<int, int>();
        private int _amount;
        public CoinCollection(int amount)
        {
            _amount = amount;
        }

        public void Update(CoinCollection collection)
        {
            foreach (var kvp in collection.Coins)
            {
                this.AddToCollection(kvp.Key, kvp.Value);
            }
        }
        public IDictionary<int, int> Coins
        {
            get
            {
                return _collection;
            }
        }
        public CoinCollection(int amount, int coin, int number)
        {
            _amount = amount;
            AddToCollection(coin, number);
        }
        public void AddToCollection(int coin, int number)
        {
            if (_collection.ContainsKey(coin))
            {
                _collection[coin] = _collection[coin] + number;
            }
            else
                _collection[coin] = number;
        }

        public CoinCollection(int amount, CoinCollection collection)
        {
            _amount = amount;
            Update(collection);
        }

        public CoinCollection(int amount, int coin, int number ,CoinCollection collection)
        {
            _amount = amount;
            Update(collection);
            AddToCollection(coin, number);
        }

        public override string ToString()
        {
            var disp = string.Empty;
            foreach (var kvp in _collection.OrderBy(x => x.Key))
            {
                disp = disp + $"Den-{kvp.Key}_Num-{kvp.Value}||";
            }
            return disp;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(CoinCollection))
                return false;
            if (obj == null)
                return false;
            var x = this;
            var y = (CoinCollection)obj;
            var b = x.Coins.All(c =>
            {
                return y.Coins.Any(z => z.Key == c.Key && z.Value == c.Value);
            });
            return b;
        }

        public int GetHashCode(CoinCollection obj)
        {
            return base.GetHashCode();
        }
    }

    public class CollectionComparer : IEqualityComparer<CoinCollection>
    {
        //public int Compare(CoinCollection x, CoinCollection y)
        //{
           
        //}

        public bool Equals(CoinCollection x, CoinCollection y)
        {
            var b = x.Coins.All(c =>
            {
                return y.Coins.Any(z => z.Key == c.Key && z.Value == c.Value);
            });
            return b;
        }

        public int GetHashCode(CoinCollection obj)
        {
            return base.GetHashCode();
        }
    }

    public static class Extensions
    {
        public static void AddOrUpdate(this Dictionary<int, List<CoinCollection>> dic, int amount, CoinCollection collection)
        {
            if (dic.ContainsKey(amount))
            {
                if(!dic[amount].Exists(x => x.Equals(collection)))
                    dic[amount].Add(collection);
            }
            else
            {
                var coll = new List<CoinCollection>();
                coll.Add(collection);
                dic[amount] = coll;
            }
        }
    }

    class Solution
    {

        protected static TextReader reader;
        protected static TextWriter writer;
        static Dictionary<int, int> _cache = new Dictionary<int, int>();

        static Dictionary<int, List<string>> _secCache = new Dictionary<int, List<string>>();
        static Dictionary<int, List<CoinCollection>> _collCache = new Dictionary<int, List<CoinCollection>>();
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
            Console.WriteLine("Completed");
            Console.ReadLine();
#endif
        }

        static void Solve()
        {
            _cache.Add(0, 0);
            var amount = Convert.ToInt32(reader.ReadLine().Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries)[0]);

            _coins = reader.ReadLine().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToArray();
            foreach (var c in _coins)
            {
                //_cache.Add(c,1);
            }
            GetNumberOfDenominations(amount);
            writer.WriteLine( _collCache.ContainsKey(amount)? _collCache[amount].Distinct(new CollectionComparer()).Count():0);
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
                    foreach (var coin in _coins)
                    {
                        {
                            if (coin == amount)
                            {
                                currentCoins.Add(coin.ToString());
                                _collCache.AddOrUpdate(amount, new CoinCollection(amount, coin, 1));
                                //total++;
                            }
                            else if (coin <= amount)
                            {
                                //if ()
                                {
                                    var toFigure = amount - coin;
                                    var den = GetNumberOfDenominations(toFigure);
                                    if (den > 0)
                                    {
                                        total = total + den;
                                        var perms = _collCache[toFigure];
                                        foreach (var coll in perms)
                                        {
                                            _collCache.AddOrUpdate(amount, new CoinCollection(amount, coin, 1, coll));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    _cache[amount] = _collCache.ContainsKey(amount) ? _collCache[amount].Count :0;
                    return total;
                }
            }
        }
    }
}
