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
        public Card(string cardName, string cardDesc, int cardNum, string cardType)
        {
            this.Name = cardName;
            Image = "";
            this.Description = cardDesc;
            this.CardNumber = cardNum;
            this.Type = cardType;
        }

        public void setName(String cardName)
        {
            Name = cardName;
        }

        public void setDesc(String cardDesc)
        {
            Description = cardDesc;
        }

        public void setNum(int cardNum)
        {
            CardNumber = cardNum;
        }

        public void setType(string cardType)
        {
            Type = cardType;
        }

        public string getName()
        {
            return Name;
        }

        public string getDesc()
        {
            return Description;
        }

        public int getNum()
        {
            return CardNumber;
        }
        public string getType()
        {
            return Type;
        }
    }

    public class MonsterCard : Card
    {
        public string Attribute { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Level { get; set; }

        public MonsterCard()
        {
            Attribute = "";
            Attack = 0;
            Defence = 0;
            Level = 0;
        }

        public MonsterCard(string cardName, string cardDesc, int cardNum, string cardType, string cardAttribute, int cardAttack, int cardDefence, int cardLevel)
        {
            this.Name = cardName;
            Image = "";
            this.Description = cardDesc;
            this.CardNumber = cardNum;
            this.Type = cardType;
            this.Attribute = cardAttribute;
            this.Attack = cardAttack;
            this.Defence = cardDefence;
            this.Level = cardLevel;
        }

        public void setAttribute(String cardAttribute)
        {
            Attribute = cardAttribute;
        }

        public void setAttack(int cardAttack)
        {
            Attack = cardAttack;
        }

        public void setDefence(int cardDef)
        {
            Defence = cardDef;
        }

        public void setLevel(int cardLevel)
        {
            Level = cardLevel;
        }

        public string getAttribute()
        {
            return Attribute;
        }

        public int getAttack()
        {
            return Attack;
        }

        public int getDefence()
        {
            return Defence;
        }
        
        public int getLevel()
        {
            return Level;
        }
    }

    public class SpellCard : Card
    {
        public string Property { get; set; }
        public string Effect { get; set; }

        public SpellCard()
        { }

        public SpellCard(string cardName, string cardDesc, int cardNum, string cardType, string cardProp, string cardEffect)
        {
            this.Name = cardName;
            Image = "";
            this.Description = cardDesc;
            this.CardNumber = cardNum;
            this.Type = cardType;
            this.Property = cardProp;
            this.Effect = cardEffect;
        }

        public void setProperty(string cardProp)
        {
            Property = cardProp;
        }

        public void setEffect(string cardEffect)
        {
            Effect = cardEffect;
        }

        public string getProperty()
        { 
            return Property;
        }

        public string getEffect()
        {
            return Effect;
        }
    }
}
