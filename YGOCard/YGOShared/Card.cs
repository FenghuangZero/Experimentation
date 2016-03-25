using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGOCardGame
{
    public class Card
    {
        public string Name { get; set; } = "";
        public string Image { get; set; } = "";
        public string Description { get; set; } = "";
        public int Number { get; set; } = 0;
        public string Type { get; set; } = "";
        public string Attribute { get; set; } = "";
        public int Attack { get; set; } = 0;
        public int Defence { get; set; } = 0;
        public int Level { get; set; } = 0;
        public bool FaceUp { get; set; } = false;
        public bool Horizontal { get; set; } = false;
        public string Location { get; set; } = "";

        // Constructors
        public Card()
        {
            Name = "";
            Image = "";
            Description = "";
            Number = 0;
            Type = "";
            Attribute = "";
            Attack = 0;
            Defence = 0;
            Level = 0;
            FaceUp = false;
            Horizontal = false;
            Location = "";
        }

        public Card(string cardName, string cardDesc, int cardNum, string cardType, string cardAttribute)
        {
            this.Name = cardName;
            this.Description = cardDesc;
            this.Number = cardNum;
            this.Type = cardType;
            this.Attribute = cardAttribute;
        }

        public Card(string cardName, string cardDesc, int cardNum, string cardType, string cardAttribute, int cardAttack, int cardDefence, int cardLevel)
        {
            this.Name = cardName;
            this.Description = cardDesc;
            this.Number = cardNum;
            this.Type = cardType;
            this.Attribute = cardAttribute;
            this.Attack = cardAttack;
            this.Defence = cardDefence;
            this.Level = cardDefence;
        }
    }
}