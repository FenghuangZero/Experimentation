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
        public List<Card> MainDeck = new List<Card>();
        public List<Card> ExtraDeck = new List<Card>();
        public List<Card> SideDeck = new List<Card>();
        public List<Card> MyHand = new List<Card>();
        public List<Card> Monster = new List<Card>();
        public List<Card> Spell = new List<Card>();
        public Card[] MonsterZone = new Card[5];
        public Card[] SpellZone = new Card[5];
        public Card[][] Deck = new Card[3][] {new Card[60], new Card[15], new Card[15] };
        public Card[] Hand = new Card[60];
        public Card[] Graveyard = new Card[75];
        

        /// <summary>
        /// Moves a single card from the deck to the hand.
        /// </summary>
        public void draw()
        {
            Debug.WriteLine("{0} drew {1}", this.Name,  this.Deck[0][0].Name);
            move(Deck[0], Hand, Deck[0][0], 0);            
            for (var i = 0; i < (Deck[0].Length - 1); i++)
            {
                Deck[0][i] = Deck[0][i + 1];
            }
            Deck[0][Deck[0].Length - 1] = null;
        }
        
        /// <summary>
        /// Moves a monster from the hand to the field in face up attack position.
        /// </summary>
        /// <param name="m">The Card object that is being summoned.</param>
        /// <param name="i">The position of the card in the hand prior to moving.</param>
        public void summon(Card[] m, int i)
        {
            m[i].FaceUp = true;
            m[i].Horizontal = false;
            Debug.WriteLine("{0} summons {1} in attack position.", this.Name, m[i].Name);
            move(Hand, MonsterZone, m[i], i);            
        }

        /// <summary>
        /// Moves a monster from the hand to the field in face down defence position.
        /// </summary>
        /// <param name="m">The Card object that is being summoned.</param>
        /// <param name="i">The position of the card in the hand prior to moving.</param>
        public void set(Card[] m, int i)
        {
            m[i].FaceUp = false;
            m[i].Horizontal = true;
            Debug.WriteLine("{0} summons a monster in face down defence position.", this.Name);
            move(Hand, MonsterZone, m[i], i);            
        }

        /// <summary>
        /// Changes the monster on the field to the opposite position
        /// </summary>
        /// <param name="m">The Card object that is being switched.</param>
        public void switchPosition(Card m)
        {
            switch (m.Horizontal)
            {
                case false:
                    m.Horizontal = true;
                    Debug.WriteLine("{0} switches {1} to defence position.", this.Name, m.Name);
                    break;
                case true:
                    m.Horizontal = false;
                    Debug.WriteLine("{0} switches {1} to defence position.", this.Name, m.Name);
                    break;
            }
        }

        /// <summary>
        /// Selects a monster to attack the opponent's life points directly.
        /// </summary>
        /// <param name="a">The attacking monster Card.</param>
        /// <param name="ai">The position on the field of the attacking monster.</param>
        /// <param name="o">The player that is being attacked.</param>
        public void attackDirectly(Card[] a, int ai, Player o)
        {
            Debug.WriteLine("{0} attack {1} directly with {2}.", this.Name, o.Name, a[ai].Name);
            o.LifePoints -= a[ai].ATK;
            Debug.WriteLine("{0}'s life points are {1}.", o.Name, o.LifePoints);
        }

        /// <summary>
        /// Selects a monster to attack an opponent's monster.
        /// </summary>
        /// <param name="a">The attacking monster Card.</param>
        /// <param name="ai">The position on the field of the attacking monster.</param>
        /// <param name="o">The player that owns the defending monster.</param>
        /// <param name="d">The defending monster Card.</param>
        /// <param name="di">The position on the field of the defending monster.</param>
        public void attackMonster(Card[] a, int ai, Player o, Card[] d, int di)
        {
            Debug.WriteLine("{0} attacks {1}'s {2} with {3}.", this.Name, o.Name, d[di].Name, a[ai].Name);
            switch (d[di].Horizontal)
            {
                case false:
                    if (a[ai].ATK > d[di].ATK)
                    {
                        var damage = a[ai].ATK - d[di].ATK;
                        o.LifePoints -= damage;
                        Debug.WriteLine("{0} takes {1} points of damage.", o.Name, damage);
                        Debug.WriteLine("{0}'s life points are {1}.", o.Name, o.LifePoints);
                        Debug.WriteLine("{0} was destroyed.", d[di].Name);
                        o.move(o.MonsterZone, o.Graveyard, d[di], di);
                    }
                    else if (a[ai].ATK == d[di].ATK)
                    {
                        Debug.WriteLine("Both monsters were destroyed.");
                        move(MonsterZone, Graveyard, a[ai], ai);
                        o.move(o.MonsterZone, o.Graveyard, d[di], di);
                    }
                    else
                    {
                        var damage = d[di].ATK - a[ai].ATK;
                        LifePoints -= damage;
                        Debug.WriteLine("{0} takes {1} points of damage.", this.Name, damage);
                        Debug.WriteLine("{0}'s life points are {1}.", this.Name, this.LifePoints);
                        Debug.WriteLine("{0} was destroyed.", a[ai].Name);
                        move(MonsterZone, Graveyard, a[ai], ai);
                    }
                    break;
                case true:
                    if (a[ai].ATK > d[di].DEF)
                    {
                        Debug.WriteLine("{0} was destroyed.", d[di].Name);
                        o.move(o.MonsterZone, o.Graveyard, d[di], di);
                    }
                    else if (a[ai].ATK == d[di].DEF)
                    {
                        Debug.WriteLine("{0} endured.", d[di].Name);
                    }
                    else
                    {
                        var damage = d[di].DEF - a[ai].ATK;
                        LifePoints -= damage;
                        Debug.WriteLine("{0} takes {1} points of damage.", this.Name, damage);
                        Debug.WriteLine("{0}'s life points are {1}.", this.Name, this.LifePoints);
                    }
                    break;
            }           
        }

        /// <summary>
        /// Moves a card from one location to another.
        /// </summary>
        /// <param name="s">The starting location</param>
        /// <param name="e">The ending location</param>
        /// <param name="c">The card to be moved.</param>
        /// <param name="si">The position of the card prior to moving.</param>
        public void move(Card[] s, Card[] e, Card c, int si)
        {
            int ei = Array.IndexOf(e, null);
            e[ei] = c;
            s[si] = null;
        }

        /// <summary>
        /// Creates a new player with default life points and an empty field.
        /// </summary>
        public Player()
        {
            Name = "";
            LifePoints = 8000;
            for (var i = 0; i < 5; i++)
            {
                MonsterZone[i] = null;
                SpellZone[i] = null;
            }
        }

        /// <summary>
        /// Creates a new player with default life points and an empty field.
        /// </summary>
        /// <param name="n">The name of the player.</param>
        public Player(string n)
        {
            Name = n;
            LifePoints = 8000;
            for (var i = 0; i < 5; i++)
            {
                MonsterZone[i] = null;
                SpellZone[i] = null;
            }
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
        public static void Draw<T>(this Queue<T> queue)
        {
            queue.Dequeue();
        }
    }

}
