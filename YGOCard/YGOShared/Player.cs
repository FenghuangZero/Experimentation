using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace YGOShared
{
    /// <summary>
    /// Provides behaivour for players and allows iteraction with cards.
    /// </summary>
    class Player
    {
        public string Name { get; set; }
        public int LifePoints { get; set; }
        public Queue<Card> MainDeck = new Queue<Card>();
        public List<Card> ExtraDeck = new List<Card>();
        public List<Card> SideDeck = new List<Card>();
        public List<Card> Hand = new List<Card>();
        public List<Card> MonsterZone = new List<Card>();
        public List<Card> SpellZone = new List<Card>();
        public Queue<Card> Graveyard = new Queue<Card>();
        
        /// <summary>
        /// Creates a new player with default life points and an empty field.
        /// </summary>
        public Player()
        {
            Name = "";
            LifePoints = 8000;
            MonsterZone.Clear();
            SpellZone.Clear();
        }

        /// <summary>
        /// Creates a new player with default life points and an empty field.
        /// </summary>
        /// <param name="n">The name of the player.</param>
        public Player(string n)
        {
            Name = n;
            LifePoints = 8000;
            MonsterZone.Clear();
            SpellZone.Clear();
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this Queue<T> queue)
        {
            var rng = new Random();
            var list = new List<T>();
            for (var i = 0; i < queue.Count; i++)
                list.Add(queue.Dequeue());

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
                queue.Enqueue(list[n]);
            }
            
        }

        public static void draw(this Player p)
        {
            p.Hand.Add(p.MainDeck.Dequeue());
            Debug.WriteLine("{0} drew {1}", p.Name, p.Hand.Last().Name);
        }

        public static void draw(this Player p, int n)
        {
            for (var i = 0; i < n; i++)
            {
                p.Hand.Add(p.MainDeck.Dequeue());
                Debug.WriteLine("{0} drew {1}", p.Name, p.Hand.Last().Name);
            }
        }

        public static void discard(this Player p, List<Card> source, Card c)
        {
            source.Remove(c);
            p.Graveyard.Enqueue(c);
        }

        public static void summon(this Player p, List<Card> source, Card c)
        {
            source.Remove(c);
            c.FaceUp = true;
            c.Horizontal = false;
            p.MonsterZone.Add(c);
        }

        public static void set(this Player p, List<Card> source, Card c)
        {
            source.Remove(c);
            c.FaceUp = false;
            c.Horizontal = true;
            p.MonsterZone.Add(c);
        }

        public static void switchPosition(this Card c)
        {
            switch (c.Horizontal)
            {
                case false:
                    c.Horizontal = true;
                    break;
                case true:
                    c.Horizontal = false;
                    break;
            }
        }

        public static void attackPlayer(this Player p, Card a, Card d, Player dp)
        {
            dp.LifePoints -= a.ATK;
        }

        public static void attackMonster(this Player p, Card a, Card d, Player dp)
        {
            switch (d.Horizontal)
            {
                case false:
                    if (a.ATK > d.ATK)
                    {
                        dp.LifePoints -= a.ATK - d.ATK;
                        dp.discard(dp.MonsterZone, d);
                    }
                    else if (a.ATK == d.ATK)
                    {
                        p.discard(p.MonsterZone, a);
                        dp.discard(dp.MonsterZone, d);
                    }
                    else
                    {
                        p.LifePoints -= d.ATK - a.ATK;
                        p.discard(p.MonsterZone, a);
                    }
                    break;
                case true:
                    if (a.ATK > d.DEF)
                    {
                        p.discard(p.MonsterZone, d);
                    }
                    else if (a.ATK == d.DEF)
                    { }
                    else
                    {
                        p.LifePoints -= d.DEF - a.ATK;
                    }
                    break;
            }
        }

    }

}
