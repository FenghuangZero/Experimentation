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
        public string Name { get; set; }
        public string Attribute { get; set; }
        public string Icon { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }
        public string Type { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public string Text { get; set; }
        public bool FaceUp { get; set; }
        public bool Horizontal { get; set; }
        public string Location { get; set; }

        /// <summary>
        /// Initializes the class with default values.
        /// </summary>
        public Card()
        {
            Name = "";
            Attribute = "";
            Icon = "";
            Level = 0;
            Rank = 0;
            Type = "";
            Attack = 0;
            Defence = 0;
            Text = "";
            FaceUp = false;
            Horizontal = false;
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
        public Card(string n, string a, int l, string t, int atk, int def, string txt)
        {
            this.Name = n;
            this.Attribute = a;
            this.Level = l;
            this.Type = t;
            this.Attack = atk;
            this.Defence = def;
            this.Text = txt;
        }
    }
}