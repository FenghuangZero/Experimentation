using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGOShared
{
    /// <summary>
    /// Provides behaviour for Card objects.
    /// </summary>
    public class Card
    {
        private int ID;
        private string Name;
        private string Attribute;
        private string Icon;
        private int Level;
        private int Rank;
        private int PendulumScale;
        private string PendulumEffect;
        private string MonsterType;
        private string CardType;
        private int ATK;
        private int DEF;
        private string CardText;
        /// <summary>
        /// A unique identifier for each card.
        /// </summary>
        public int id { get { return ID; } }
        /// <summary>
        /// A string to hold any temporary changes to a card's name.
        /// </summary>
        public string nameChange { get; set; }
        /// <summary>
        /// The visible name of the card.
        /// </summary>
        public string nameOnField
        {
            get
            {
                if (FaceUp)
                    if (nameChange != "")
                        return Name;
                    else return nameChange;
                else return "A Card";

            }
        }
        /// <summary>
        /// A classical elemental attribute (Fire, Water, etc.) used to differentiate cards for certain card rules.
        /// </summary>
        public string attribute { get { return Attribute; } }
        /// <summary>
        /// A marker to identify certain cards that can be played in ways different to regular cards (eg. equip, counter, field, quick).
        /// </summary>
        public string icon { get { return Icon; } }
        /// <summary>
        /// A numerical indication of strength of a card. Cards of a level higher than 4 require discarding a card that is already in play.
        /// Cards of a level higher than 6 likewise require discarding two cards.
        /// </summary>
        public int level { get { return Level; } }
        /// <summary>
        /// Like level, a numerical indication of strength, however only applies to "Xyz" cards. These cards do not have a level and have
        /// their own rules for being played.
        /// </summary>
        public int rank { get { return Rank; } }
        /// <summary>
        /// Two "Pendulum" cards can be used to quickly play multiple cards who's level is between (inclusive) the two "pendulum" cards' 
        /// pendulum scale values.
        /// </summary>
        public int pendulumScale { get { return PendulumScale; } }
        /// <summary>
        /// A special ruling that occurs when a "pendulum" card is in play in a specific position on the board.
        /// </summary>
        public string pendulumEffect { get { return PendulumEffect; } }
        /// <summary>
        /// A descriptor of what is depicted on the card, used in certain card rulings (eg. Warrior, Spellcaster, Reptile etc.).
        /// </summary>
        public string monsterType { get { return MonsterType; } }
        /// <summary>
        /// A marker to identify cards with designated rulings (eg. Effect, Xyz, Pendulum, Fusion etc.).
        /// </summary>
        public string cardType { get { return CardType; } }
        /// <summary>
        /// A value to indicate any changes to a card's "Attack" value.
        /// </summary>
        public int atkChanges { get; set; }
        /// <summary>
        /// The visible "Attack" value of a card.
        /// </summary>
        public int atkOnField
        {
            get
            {
                if (FaceUp == true)
                    return ATK + atkChanges;
                else
                    return 0;
            }
        }
        /// <summary>
        /// A value to indicate any changes to a card's "Defence" value.
        /// </summary>
        public int defChanges { get; set; }
        /// <summary>
        /// The visible "Defence" value of a card.
        /// </summary>
        public int defOnField
        {
            get
            {
                if (FaceUp == true)
                    return DEF + defChanges;
                else
                    return 0;
            }
        }
        /// <summary>
        /// Text displayed on a card. For "Effect" cards, this includes rulings specific to that card.
        /// </summary>
        public string cardText { get { return CardText; } }
        /// <summary>
        /// A boolean to control whether or not a card is face up and visible on the board.
        /// </summary>
        public bool FaceUp { get; set; }
        /// <summary>
        /// A boolean to control whether a card is positioned horizontally (in relation to the players) on the board. Horizontal cards
        /// are refered to as being in "defence position", and the primary value used to determine their success in a battle is "Defence".
        /// </summary>
        public bool Horizontal { get; set; }

        /// <summary>
        /// Initializes the class with default values.
        /// </summary>
        public Card()
        {
            ID = 0;
            Name = "";
            Attribute = "";
            Icon = "";
            Level = 0;
            Rank = 0;
            PendulumScale = 0;
            PendulumEffect = "";
            MonsterType = "";
            ATK = 0;
            DEF = 0;
            CardText = "";
            FaceUp = false;
            Horizontal = false;
        }

        /// <summary>
        /// Initializes the class using default values for all but the unique identifier.
        /// </summary>
        /// <param name="id"></param>
        public Card(int id)
        {
            ID = id;
        }

        /// <summary>
        /// Initializes the class with all necessary values.
        /// </summary>
        /// <param name="i">The card's unique identifier</param>
        /// <param name="n">The card's name.</param>
        /// <param name="a">The card's elemental attribute.</param>
        /// <param name="ico">The card's icon.</param>
        /// <param name="l">The card's level.</param>
        /// <param name="r">The card's rank.</param>
        /// <param name="ps">The card's pendulum scale.</param>
        /// <param name="pe">The card's pendulum effect.</param>
        /// <param name="mt">The card's monster type.</param>
        /// <param name="ct">The card type.</param>
        /// <param name="atk">The card's attack value.</param>
        /// <param name="def">The card's defence value.</param>
        /// <param name="txt">The text written in the card Text Box.</param>
        public Card(int i, string n, string a, string ico, int l, int r, int ps, string pe, string mt, string ct, int atk, int def, string txt)
        {
            ID = i;
            Name = n;
            Attribute = a;
            Icon = ico;
            Level = l;
            Rank = r;
            PendulumScale = ps;
            PendulumEffect = pe;
            MonsterType = mt;
            CardType = ct;
            ATK = atk;
            DEF = def;
            CardText = txt;
        }
    }

    public class CardReader
    {
        static string[] parseables = new string[] { "This card", "gains", "for each"};
        bool[] present = new bool[parseables.Count()];
        List<string> split = new List<string>();
        bool targetSelf;
        Card targetCard;
        bool targetGainsATK;
        bool targetGainsDef;
        bool targetGainsLifePoints;
        int value;
        int count;
        string effect;
        string condition;
        public void parseCard(Player p, Card c)
        {
            // split = CardText.Split(' ').ToList();
            foreach (var s in parseables)
            {
                if (c.cardText.Contains(s))
                {
                    var i = Array.IndexOf(parseables, s);
                    present[i] = true;
                    Debug.WriteLine("Card contains: {0}", s);                   
                }
            }

            Debug.WriteLine("present[0]: {0}", present[0]);
            Debug.WriteLine("present[1]: {0}", present[1]);
            Debug.WriteLine("present[2]: {0}", present[2]);

            if (present[0])
            {
                targetSelf = true;
                Debug.WriteLine("Target self");
            }

            if (present[1])
            {
                Debug.WriteLine("Increase ATK or DEF");
                var i = c.cardText.IndexOf(parseables[1]);
                var subText = c.cardText.Substring(i, c.cardText.IndexOf('.') - i);
                if (subText.IndexOf(',') != -1)
                    subText = subText.Substring(0, subText.IndexOf(','));
                if (subText.IndexOf("for each") != -1)
                    subText = subText.Substring(0, subText.IndexOf("for"));

                var words = subText.Split(' ');
                bool first = int.TryParse(words[1], out value);
                Debug.WriteLine("By {0}", value);
                if (first)
                {
                    if (words[2] == "ATK" && (words[3] != "and" || words[3] != "or"))
                    {
                        targetGainsATK = true;
                        Debug.WriteLine("Increase attack.");
                    }
                    else if (words[2] == "DEF" && (words[3] != "and" || words[3] != "or"))
                        targetGainsDef = true;
                    else if (words[2] == "ATK" && words[3] == "and" && words[4] == "DEF")
                    {
                        targetGainsATK = true;
                        targetGainsDef = true;
                    }
                    else if (words[2] == "life")
                        targetGainsLifePoints = true;
                }
                if (words[1] == "ATK" && (words[2] != "and" || words[2] != "or"))
                    targetGainsATK = true;
                else if (words[1] == "DEF" && (words[2] != "and" || words[3] != "or"))
                    targetGainsDef = true;
                else if (words[1] == "ATK" && words[2] == "and" && words[3] == "DEF")
                {
                    targetGainsATK = true;
                    targetGainsDef = true;
                }
                else if (words[1] == "life")
                    targetGainsLifePoints = true;
            }

            if (present[2])
            {
                var i = c.cardText.IndexOf(parseables[2]);
                var subText = c.cardText.Substring(i, c.cardText.IndexOf('.') - i);
                var cardName = subText.Substring(subText.IndexOf('"') + 1, subText.LastIndexOf('"') - subText.IndexOf('"') - 1);
                
                if (subText.Contains("you control"))
                {
                    var num = p.MonsterZone.FindAll(n => n.nameOnField == cardName);
                    count = num.Count;
                }
            }

            if (targetGainsATK && targetSelf)
                c.atkChanges += value * count;
            /*
            var i = 0;
            var modifier = "";
            var value = 0;
            if (split.Contains("gains"))
            {
                i = split.FindIndex(x => x == "gains");
                if (int.TryParse(split[i + 1], out value))
                {
                    value = int.Parse(split[i + 1]);
                    modifier = split[i + 2]; if (split[i + 3] == "and")
                        modifier = split[i + 2] + split[i + 3] + split[i + 4];
                }
                else
                {
                    value = int.Parse(split[i + 2]);
                    modifier = split[i + 1];                }
                if (modifier == "ATK" || modifier == "DEF")
                    value = int.Parse(split[i + 2]);
                gain(p, c, modifier, value);
            }
            */
        }

        public void gain(Player p, Card c, string gainer, int amount)
        {
            if (gainer == "ATK")
                c.atkChanges += amount;
            if (gainer == "DEF")
                c.defChanges += amount;
            if (gainer == "ATKandDEF")
            {
                c.atkChanges += amount;
                c.defChanges += amount;
            }
            if (gainer == "Life")
                p.LifePoints += amount;
        }

        public CardReader()
        {
        }
    }
}