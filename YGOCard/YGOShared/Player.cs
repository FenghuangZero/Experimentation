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
            //Console.WriteLine(this.Name + " drew " + Deck[0].Name);
            this.move(Deck, Hand, Deck[0], 0);            
            for (int i = 0; i < (Deck.Length - 1); i++)
            {
                this.Deck[i] = this.Deck[i + 1];
            }
            this.Deck[Deck.Length - 1] = null;
        }


        public void summon(Card[] monster, int indexLocation)
        {
            monster[indexLocation].Position = "Attack";
            //Console.WriteLine(this.Name + " summons " + monster[indexLocation].Name + " in " + monster[indexLocation].Position + " position.");
            this.move(Hand, MonsterZone, monster[indexLocation], indexLocation);            
        }

        public void set(Card[] monster, int indexLocation)
        {
            monster[indexLocation].Position = "Defence";
            //Console.WriteLine(this.Name + " summons " + "a monster in face down" + monster[indexLocation].Position + " position.");
            this.move(Hand, MonsterZone, monster[indexLocation], indexLocation);            
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
            //Console.WriteLine(this.Name + " switches " + monster.Name + " to " + monster.Position + "position.");
        }

        public void attackDirectly(Card[] attacker, int attackerIndex, Player opponent)
        {
            //Console.WriteLine(this.Name + " attacks " + opponent.Name + " directly with " + attacker[attackerIndex].Name + ".");
            opponent.LifePoints -= attacker[attackerIndex].Attack;
            //Console.WriteLine(opponent.Name + "'s life points are " + opponent.LifePoints + ".");
        }

        public void attackMonster(Card[] attacker, int attackerIndex, Player opponent, Card[] defender, int defenderIndex)
        {
            //Console.WriteLine(this.Name + " attacks " + opponent.Name + "'s " + defender[defenderIndex].Name + " with " + attacker[attackerIndex].Name + ".");
            if (defender[defenderIndex].Position == "Attack")
            {
                if (attacker[attackerIndex].Attack > defender[defenderIndex].Attack)
                {
                    int damage = attacker[attackerIndex].Attack - defender[defenderIndex].Attack;
                    opponent.LifePoints -= damage;
                    //Console.WriteLine(opponent.Name + " takes " + damage + " points of damage");
                    //Console.WriteLine(opponent.Name + "'s Life points are " + opponent.LifePoints);
                    //Console.WriteLine(defender[defenderIndex].Name + " was destroyed.");
                    opponent.move(opponent.MonsterZone, opponent.Graveyard, defender[defenderIndex], defenderIndex);
                }
                else if (attacker[attackerIndex].Attack == defender[defenderIndex].Attack)
                {
                    //Console.WriteLine("Both monsters were destroyed.");

                    this.move(this.MonsterZone, this.Graveyard, attacker[attackerIndex], attackerIndex);
                    opponent.move(opponent.MonsterZone, opponent.Graveyard, defender[defenderIndex], defenderIndex);
                }
                else
                {
                    int damage = defender[defenderIndex].Attack - attacker[attackerIndex].Attack;
                    this.LifePoints -= damage;
                    //Console.WriteLine(this.Name + " takes " + damage + " points of damage");
                    //Console.WriteLine(this.Name + "'s Life points are " + this.LifePoints);
                    //Console.WriteLine(attacker[attackerIndex].Name + " was destroyed.");
                    this.move(this.MonsterZone, this.Graveyard, attacker[attackerIndex], attackerIndex);
                }
            }
            else
            {
                if (attacker[attackerIndex].Attack > defender[defenderIndex].Defence)
                {
                    //Console.WriteLine(defender[defenderIndex].Name + " was destroyed.");
                    opponent.move(opponent.MonsterZone, opponent.Graveyard, defender[defenderIndex], defenderIndex);
                }
                else if (attacker[attackerIndex].Attack == defender[defenderIndex].Defence)
                {
                    //Console.WriteLine(defender[defenderIndex].Name + " endured.");
                }
                else
                {
                    int damage = defender[defenderIndex].Defence - attacker[attackerIndex].Attack;
                    this.LifePoints -= damage;
                    //Console.WriteLine(this.Name + " takes " + damage + " ponts of damage");
                    //Console.WriteLine(this.Name + "'s Life points are " + this.LifePoints);
                }
            }
        }

        public void move(Card[] startLocation, Card[] endLocation, Card card, int startLocationIndex)
        {
            for (int i = 0; i < endLocation.Length;)
            {
                if (endLocation[i] == null)
                {
                    endLocation[i] = card;
                    startLocation[startLocationIndex] = null;
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
