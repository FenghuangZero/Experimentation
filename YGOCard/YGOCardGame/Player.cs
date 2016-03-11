using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGOCardGame
{
    class Player
    {
        public string Name { get; set; }
        public int LifePoints { get; set; }
        public Card[] MonsterZone = new Card[5];
        public Card[] SpellZone = new Card[5];
        public Card[] Deck = new Card[60];
        public Card[] Hand = new Card[60];
        public Card[] Graveyard = new Card[75];

        // Methods
        public void draw()
        {
            for (int i = 0; i < 8;)
            {
                if (Hand[i] == null)
                {
                    Hand[i] = Deck[0];
                    Hand[i].Position = ("Hand[" + i + "");
                    Console.WriteLine(this.Name + " drew " + Hand[i].Name);
                    for (int j = 0; j < (Deck.Length - 1); j++)
                    {
                        this.Deck[j] = this.Deck[j + 1];
                    }
                    this.Deck[Deck.Length - 1] = null;
                    break;
                }
                else
                {
                    i++;
                }
            }
        }

        public void summon(Card monster)
        {
            for (int i = 0; i < 5;)
            {
                if (MonsterZone[i] == null)
                {
                    MonsterZone[i] = monster;
                    MonsterZone[i].Position = "Attack";
                    MonsterZone[i].Location = ("MonsterZone[" + i + "]");
                    Console.WriteLine(this.Name + " summons " + this.MonsterZone[i].Name + " in " + monster.Position + " position.");
                    break;
                }
                else
                    i++;
                
            }
        }

        public void set(Card monster)
        {
            for (int i = 0; i < 5;)
            {
                if (MonsterZone[i] == null)
                {
                    MonsterZone[i] = monster;
                    MonsterZone[i].Position = "Defence";
                    MonsterZone[i].Location = ("MonsterZone[" + i + "]");
                    Console.WriteLine(this.Name + " summons " + "a monster in face down" + monster.Position + " position.");
                    break;
                }
                else
                    i++;

            }
        }

        public void switchPosition(Card monster)
        {
            if (monster.Position == "Attack")
            {
                monster.Position = "Defence";
            }
            else if (monster.Position == "Defence")
            {
                monster.Position = "Attack";
            }
            Console.WriteLine(this.Name + " switches " + monster.Name + " to " + monster.Position + "position.");
        }

        public void attackDirectly(Card attacker, Player opponent)
        {
            Console.WriteLine(this.Name + " attacks " + opponent.Name + " directly with " + attacker.Name + ".");
            opponent.LifePoints -= attacker.Attack;
            Console.WriteLine(opponent.Name + "'s life points are " + opponent.LifePoints + ".");
        }

        public void attackMonster(Card attacker, Player opponent, Card defender)
        {
            Console.WriteLine(this.Name + " attacks " + opponent.Name + "'s " + defender.Name + " with " + attacker.Name + ".");
            if (defender.Position == "Attack")
            {
                if (attacker.Attack > defender.Attack)
                {
                    int damage = attacker.Attack - defender.Attack;
                    opponent.LifePoints -= damage;
                    Console.WriteLine(opponent.Name + " takes " + damage + " points of damage");
                    Console.WriteLine(opponent.Name + "'s Life points are " + opponent.LifePoints);
                    Console.WriteLine(defender.Name + " was destroyed.");
                    opponent.discardToGraveyard(defender);
                }
                else if (attacker.Attack == defender.Attack)
                {
                    Console.WriteLine("Both monsters were destroyed.");
                    this.discardToGraveyard(attacker);
                    opponent.discardToGraveyard(defender);
                }
                else
                {
                    int damage = defender.Attack - attacker.Attack;
                    this.LifePoints -= damage;
                    Console.WriteLine(this.Name + " takes " + damage + " points of damage");
                    Console.WriteLine(this.Name + "'s Life points are " + this.LifePoints);
                    Console.WriteLine(attacker.Name + " was destroyed.");
                    this.discardToGraveyard(attacker);
                }
            }
            else
            {
                if (attacker.Attack > defender.Defence)
                {
                    Console.WriteLine(defender.Name + " was destroyed.");
                    opponent.discardToGraveyard(defender);
                }
                else if (attacker.Attack == defender.Defence)
                {
                    Console.WriteLine(defender.Name + " endured.");
                }
                else
                {
                    int damage = defender.Defence - attacker.Attack;
                    this.LifePoints -= damage;
                    Console.WriteLine(this.Name + " takes " + damage + " ponts of damage");
                    Console.WriteLine(this.Name + "'s Life points are " + this.LifePoints);
                }
            }
        }

        public void discardToGraveyard(Card card)
        {
            for (int i = 0; i < Graveyard.Length;)
            {
                if (Graveyard[i] == null)
                {
                    Graveyard[i] = card;
                    Graveyard[i].Position = ("Graveyard[" + i + "]");
                    break;
                }
                else
                    i++;
            }
        }
        // Constructors
        public Player()
        {
            Name = "";
            LifePoints = 8000;
            for (int i = 0; i < 5; i++)
            {
                MonsterZone[i] = null;
                SpellZone[i] = null;
            }
        }
        public Player(string name)
        {
            Name = name;
            LifePoints = 8000;
            for (int i = 0; i < 5; i++)
            {
                MonsterZone[i] = null;
                SpellZone[i] = null;
            }
        }
    }

}
