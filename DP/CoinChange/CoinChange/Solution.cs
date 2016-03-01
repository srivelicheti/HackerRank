using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinChange
{

    public class CoinCollection
    {
        public static TextWriter writer;
        private readonly Dictionary<int, int> _collection = new Dictionary<int, int>();
        public readonly int  _amount;
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
        public Dictionary<int, int> Coins
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

        public CoinCollection(int amount, int coin, int number, CoinCollection collection)
        {
            //var sw = Stopwatch.StartNew();
            _amount = amount;
            foreach (var kvp in collection.Coins)
            {
                _collection[kvp.Key] = kvp.Value;
            }
            //Update(collection);
            //_collection = collection
            AddToCollection(coin, number);
            //sw.Stop();
          //  writer.WriteLine($"???????????? Created New Coll {amount}");
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
            //if (obj.GetType() != typeof(CoinCollection))
            //    return false;
            //if (obj == null)
            //    return false;
           
            var y = (CoinCollection)obj;
            if (Coins.Count != y.Coins.Count)
                return false;
            if (_amount != y._amount)
                return false;
            return Coins.All(c => y.Coins.ContainsKey(c.Key) && y.Coins[c.Key] == c.Value);
        }

        public override int GetHashCode()
        {
            return string.Join("_", Coins.Keys).GetHashCode();
        }
    }

    public class CollectionComparer : IEqualityComparer<CoinCollection>
    {
        public bool Equals(CoinCollection x, CoinCollection y)
        {
            if (x._amount != y._amount)
                return false;
            if (x.Coins.Count != y.Coins.Count)
                return false;
            return x.Coins.All(c => y.Coins.ContainsKey(c.Key) && y.Coins[c.Key] == c.Value);
        }

        public int GetHashCode(CoinCollection obj)
        {
            return string.Join("_", obj.Coins.Keys).GetHashCode();
        }
    }

    public static class Extensions
    {
        public static TextWriter Writer;
        public static void AddOrUpdate(this Dictionary<int, List<CoinCollection>> dic, int amount, CoinCollection collection)
        {
            if (dic.ContainsKey(amount))
            {
                var temp = dic[amount];
                if (!temp.Exists(x => x.Equals(collection)))
                    temp.Add(collection);
            }
            else
            {
                var coll = new List<CoinCollection>();
                coll.Add(collection);
                dic[amount] = coll;
            }
        }

        public static void AddOrUpdate(this Dictionary<int, List<CoinCollection>> dic, int amount, List<CoinCollection> collection)
        {
            if (dic.ContainsKey(amount))
            {
                var temp = dic[amount];
                var sw = new Stopwatch();
                foreach (var coinCollection in collection)
                {
                    sw.Start();
                    if (!temp.Exists(x => x.Equals(coinCollection)))
                    {
                        sw.Stop();
                        temp.Add(coinCollection);
                    }
                    else
                    {
                        sw.Stop();
                    }
                }
                Writer.WriteLine($"############## CHecked Exists for {sw.ElapsedMilliseconds}");
            }
            else
            {
                //var coll = new List<CoinCollection>();
                //coll.Add(collection);
                //dic[amount] = coll;
                dic[amount] = collection;
            }
        }

        public static void AddOrUpdate(this Dictionary<int, HashSet<CoinCollection>> dic, int amount, List<CoinCollection> collection)
        {
            if (dic.ContainsKey(amount))
            {
                var temp = dic[amount];
                var sw = new Stopwatch();
                foreach (var coinCollection in collection)
                {
                    sw.Start();
                    
                    if (!temp.Contains(coinCollection) )
                    {
                        sw.Stop();
                        temp.Add(coinCollection);
                    }
                    else
                    {
                        temp.
                    }
                }
                Writer.WriteLine($"############## CHecked Exists for {sw.ElapsedMilliseconds}");
            }
            else
            {
                //var coll = new List<CoinCollection>();
                //coll.Add(collection);
                //dic[amount] = coll;
                dic[amount] = collection;
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
           // writer = Console.Out;
             writer = new StreamWriter("..\\..\\output.txt",false);
            
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
            Extensions.Writer = writer;
            CoinCollection.writer = writer;
            _cache.Add(0, 0);
            var amount = Convert.ToInt32(reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);

            _coins = reader.ReadLine().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToArray();
            foreach (var c in _coins)
            {
                //_cache.Add(c,1);
            }
            GetNumberOfDenominations(amount);
            writer.WriteLine(_collCache.ContainsKey(amount) ? _collCache[amount].Distinct(new CollectionComparer()).Count() : 0);
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
                    var sw = Stopwatch.StartNew();
                    var total = 0;
                    List<int> currentCoins = new List<int>();
                    foreach (var coin in _coins)
                    {
                        {
                            if(currentCoins.Contains(coin))
                                continue;
                            if (coin == amount)
                            {
                                currentCoins.Add(coin);
                                _collCache.AddOrUpdate(amount, new CoinCollection(amount, coin, 1));
                                //total++;
                            }
                            else if (coin <= amount)
                            {
                                //if ()
                                {
                                    
                                    var toFigure = amount - coin;
                                   
                                    sw.Stop();
                                    var den = GetNumberOfDenominations(toFigure);
                                    sw.Start();
                                    if (den > 0)
                                    {
                                        
                                        total = total + den;
                                        var perms = _collCache[toFigure];
                                        var el = sw.ElapsedMilliseconds;
                                        //var toUse = new List<CoinCollection>();
                                        //foreach (var p in perms)
                                        //{
                                        //    currentCoins.
                                        //}
                                        var list = perms.Select(coll => new CoinCollection(amount, coin, 1, coll)).ToList();
                                        var t = sw.ElapsedMilliseconds - el;
                                        el = sw.ElapsedMilliseconds;
                                        _collCache.AddOrUpdate(amount, list);
                                        writer.WriteLine($"------Created in {t} Added|updated {amount} to cache in {(sw.ElapsedMilliseconds - el)}");
                                        //foreach (var coll in perms)
                                        //{
                                        //    var el = sw.ElapsedMilliseconds;
                                        //    _collCache.AddOrUpdate(amount, new CoinCollection(amount, coin, 1, coll));
                                        //    //Console.WriteLine($"Added|updated {amount} to cache in {(sw.ElapsedMilliseconds - el)}");
                                        //}
                                    }
                                    currentCoins.Add(coin);
                                    currentCoins.Add(toFigure);
                                }
                            }
                        }
                    }
                    sw.Stop();
                    _cache[amount] = _collCache.ContainsKey(amount) ? _collCache[amount].Count : 0;
                    var debugStr = "Calculated  for : " + amount + " in " + sw.ElapsedMilliseconds;
                    writer.WriteLine(debugStr);
                    writer.Flush();
                    Console.WriteLine(debugStr);
                    return total;
                }
            }
        }
    }
}
