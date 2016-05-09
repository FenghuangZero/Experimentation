using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGOShared
{
    /// <summary>
    /// Provides behaviour for card items.
    /// </summary>
    public class Card
    {
        int ID;
        public int id { get { return ID; } }
        string Name;
        public string nameChange { get; set; }
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
        string Attribute;
        public string attribute { get { return Attribute; } }
        string Icon;
        public string icon { get { return Icon; } }
        int Level;
        public int level { get { return Level; } }
        int Rank;
        public int rank { get { return Rank; } }
        int PendulumScale;
        public int pendulumScale { get { return PendulumScale; } }
        string PendulumEffect;
        public string pendulumEffect { get { return PendulumEffect; } }
        string MonsterType;
        public string monsterType { get { return MonsterType; } }
        string CardType;
        public string cardType { get { return CardType; } }
        int ATK;
        public int atkChanges { get; set; }
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
        int DEF;
        public int defChanges { get; set; }
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
        string CardText;
        public string cardText { get { return CardText; } }
        public bool FaceUp { get; set; }
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

        public Card(int id)
        {
            ID = id;
        }

        /// <summary>
        /// Initializes the class with the minimum required values to create a Normal Monster Card.
        /// </summary>
        /// <param name="n">The card's name.</param>
        /// <param name="a">The card's attribute.</param>
        /// <param name="l">The card's level</param>
        /// <param name="t">The card's type.</param>
        /// <param name="atk">The card's attack value.</param>
        /// <param name="def">The card's defence value.</param>
        /// <param name="txt">The description written in the card Text Box.</param>
        public Card(int i, string n, string a, string ico, int l, int r, int ps, string pe, string t, string ct, int atk, int def, string txt)
        {
            ID = i;
            Name = n;
            Attribute = a;
            Icon = ico;
            Level = l;
            Rank = r;
            PendulumScale = ps;
            PendulumEffect = pe;
            MonsterType = t;
            CardType = ct;
            ATK = atk;
            DEF = def;
            CardText = txt;
        }
    }
}