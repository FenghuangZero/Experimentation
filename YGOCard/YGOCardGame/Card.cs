using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGOCardGame
{
    public class Card
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int CardNumber { get; set; }
        public string Type { get; set; }

        public Card()
        {
            Name = "";
            Image = "";
            Description = "";
            CardNumber = 0;
            Type = "";
        }
        public Card(string cardName, string desc, int num, string type)
        {
            this.Name = cardName;
            Image = "";
            this.Description = desc;
            this.CardNumber = num;
            this.Type = type;
        }

        public void setName(String cardName)
        {
            Name = cardName;
        }

        public string getName()
        {
            return Name;
        }

        public string getDesc()
        {
        return Description;
        }
    }

    public class MonsterCard : Card
    {
        public string Attribute { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int level { get; set; }

        public MonsterCard()
        { }
    }

    public class SpellCard : Card
    {
        public string Property { get; set; }
        public string Effect { get; set; }

        public SpellCard()
        { }
    }
}
