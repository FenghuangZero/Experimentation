using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace YGOShared
{
    /// <summary>
    /// Provides behaivour for players and allows interaction with cards.
    /// </summary>
    public class Player
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
        public bool canAttack = false;
        public bool canDraw = true;
        public bool canSummon = false;
        
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

    /// <summary>
    /// A method of generating a random number in a thread-safe manner.
    /// </summary>
    public class ThreadSafeRandom
    {
        private static readonly Random Global = new Random();
        [ThreadStatic]
        private static Random Local;
        
        /// <summary>
        /// Initialises the random generator
        /// </summary>
        public ThreadSafeRandom()
        {
            if (Local == null)
            {
                int seed;
                lock (Global)
                {
                    seed = Global.Next();
                }
                Local = new Random(seed);
            }
        }
        /// <summary>
        /// Returns a nonnegative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="n">Maximum</param>
        /// <returns></returns>
        public int Next(int n)
        { return Local.Next(n); }
    }

    /// <summary>
    /// Extensions to be performed on lists of cards during a game.
    /// </summary>
    static class MyExtensions
    {
        /// <summary>
        /// Randomises the order of a list.
        /// </summary>
        /// <typeparam name="T">The type of object held in the list.</typeparam>
        /// <param name="list">The list to be randomised.</param>
        public static void Shuffle<T>(this List<T> list)
        {
            var rand = new ThreadSafeRandom();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Converts a queue to a list, then randomises the order of that list and converts the new order into a queue.
        /// </summary>
        /// <typeparam name="T">The type of object held in the queue.</typeparam>
        /// <param name="queue">The queue to be randomised.</param>
        public static void Shuffle<T>(this Queue<T> queue)
        {
            var list = new List<T>();
            list = queue.ToList();
            list.Shuffle();
            queue.Clear();
            for (int i = 0; i < list.Count; i++)
                queue.Enqueue(list.ElementAt(i));
        }

        /// <summary>
        /// Removes the first card in a player's deck and adds it to their hand.
        /// </summary>
        /// <param name="p">The player who is drawing a card.</param>
        public static void draw(this Player p)
        {
            p.Hand.Add(p.MainDeck.Dequeue());
            Debug.WriteLine("{0} drew {1}", p.Name, p.Hand.Last().nameOnField);
        }

        /// <summary>
        /// Removes a number of cards from a player's deck and adds them to their hand.
        /// </summary>
        /// <param name="p">The player who is drawing cards.</param>
        /// <param name="n">The number of cards to be drawn.</param>
        public static void draw(this Player p, int n)
        {
            for (var i = 0; i < n; i++)
            {
                p.Hand.Add(p.MainDeck.Dequeue());
                Debug.WriteLine("{0} drew {1}", p.Name, p.Hand.Last().nameOnField);
            }
        }

        /// <summary>
        /// Removes a card from a list and adds it to the graveyard.
        /// </summary>
        /// <param name="p">The player who is discarding cards.</param>
        /// <param name="source">The location from which the cards are being discarded from.</param>
        /// <param name="c">The card to be discarded.</param>
        public static void discard(this Player p, List<Card> source, Card c)
        {
            source.Remove(c);
            p.Graveyard.Enqueue(c);
        }

        /// <summary>
        /// Places a card in a player's monster zone.
        /// </summary>
        /// <param name="p">The player who is summoning a monster card.</param>
        /// <param name="source">The location from which the card is being played.</param>
        /// <param name="c">The card to be played.</param>
        public static void summon(this Player p, List<Card> source, Card c)
        {
            source.Remove(c);
            c.FaceUp = true;
            c.Horizontal = false;
            p.MonsterZone.Add(c);
            p.canSummon = false;
            Debug.WriteLine("{0} has summoned {1}.", p.Name, c.nameOnField);
        }

        /// <summary>
        /// Places a card in a player's monster zone in face-down defence position.
        /// </summary>
        /// <param name="p">The player who is summoning a monster card.</param>
        /// <param name="source">The location from which the card is being played.</param>
        /// <param name="c">The card to be played.</param>
        public static void set(this Player p, List<Card> source, Card c)
        {
            source.Remove(c);
            c.FaceUp = false;
            c.Horizontal = true;
            p.MonsterZone.Add(c);
            Debug.WriteLine("{0} has summoned a monster face down.", p.Name);
        }

        /// <summary>
        /// Switches a card's position from attack to defence position.
        /// </summary>
        /// <param name="c">The card to be switched.</param>
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

        /// <summary>
        /// Attacks a player directly with a monster card.
        /// </summary>
        /// <param name="p">The player who owns the attacking card.</param>
        /// <param name="a">The monster card that is attacking.</param>
        /// <param name="dp">The player who is the target of the attack.</param>
        public static void attackPlayer(this Player p, Card a, Player dp)
        {
            dp.LifePoints -= a.atkOnField;
            Debug.WriteLine("{0} attacks {1} directly with {2}", p.Name, dp.Name, a.nameOnField);
        }

        /// <summary>
        /// Attacks an opponent's monster card with a monster card.
        /// </summary>
        /// <param name="p">The player who owns the attacking card.</param>
        /// <param name="a">The monster card that is attacking.</param>
        /// <param name="d">The monster card that is the target of the attack.</param>
        /// <param name="dp">The player who owns the target card.</param>
        public static void attackMonster(this Player p, Card a, Card d, Player dp)
        {
            Debug.WriteLine("{0} attacks {1} with {2}", p.Name, d.nameOnField, a.nameOnField);
            d.Flip();
            switch (d.Horizontal)
            {
                case false:
                    if (a.atkOnField > d.atkOnField)
                    {
                        Debug.WriteLine("{0} was destroyed.", d.nameOnField);
                        dp.LifePoints -= a.atkOnField - d.atkOnField;
                        dp.discard(dp.MonsterZone, d);
                    }
                    else if (a.atkOnField == d.atkOnField)
                    {
                        Debug.WriteLine("Both monsters were destroyed.");
                        p.discard(p.MonsterZone, a);
                        dp.discard(dp.MonsterZone, d);
                    }
                    else
                    {
                        Debug.WriteLine("{0} was destroyed.", a.nameOnField);
                        p.LifePoints -= d.atkOnField - a.atkOnField;
                        p.discard(p.MonsterZone, a);
                    }
                    break;
                case true:
                    if (a.atkOnField > d.defOnField)
                    {
                        Debug.WriteLine("{0} was destroyed.", d.nameOnField);
                        p.discard(p.MonsterZone, d);
                    }
                    else if (a.atkOnField == d.defOnField)
                    {
                        Debug.WriteLine("{0} survived the attack.", d.nameOnField);
                    }
                    else
                    {
                        Debug.WriteLine("{0} survived the attack.", d.nameOnField);
                        p.LifePoints -= d.atkOnField - a.atkOnField;
                    }
                    break;
            }
        }

        /// <summary>
        /// Changes a card from face-down to face-up.
        /// </summary>
        /// <param name="c"></param>
        public static void Flip(this Card c)
        {
            if (c.FaceUp == false)
            {
                c.FaceUp = true;
                Debug.WriteLine("{0} was flipped face up.", c.nameOnField);
            }
        }
    }

}
