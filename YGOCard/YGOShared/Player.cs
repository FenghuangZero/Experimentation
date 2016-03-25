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
#if CONSOLE
            Console.WriteLine(this.Name + " drew " + Deck[0].Name);
#endif
            this.move(Deck, Hand, Deck[0], 0);            
            for (int i = 0; i < (Deck.Length - 1); i++)
            {
                this.Deck[i] = this.Deck[i + 1];
            }
            this.Deck[Deck.Length - 1] = null;
        }


        public void summon(Card[] monster, int indexLocation)
        {
            monster[indexLocation].FaceUp = true;
            monster[indexLocation].Horizontal = false;
#if CONSOLE
            Console.WriteLine("{0} summons {1} in attack position.", this.Name, monster[indexLocation].Name);
#endif
            this.move(Hand, MonsterZone, monster[indexLocation], indexLocation);            
        }

        public void set(Card[] monster, int indexLocation)
        {
            monster[indexLocation].FaceUp = false;
            monster[indexLocation].Horizontal = true;
#if CONSOLE
            Console.WriteLine("{0} summons a monster in face down defence position.", this.Name);
#endif
            this.move(Hand, MonsterZone, monster[indexLocation], indexLocation);            
        }

        public void switchPosition(Card monster)
        {
            switch (monster.Horizontal)
            {
                case false:
                    monster.Horizontal = true;
#if CONSOLE
                    Console.WriteLine("{0} switches {1} to defence position.", this.Name, monster.Name);
#endif
                    break;
                case true:
                    monster.Horizontal = false;
#if CONSOLE
                    Console.WriteLine("{0} switches {1} to defence position.", this.Name, monster.Name);
#endif
                    break;
            }
        }

        public void attackDirectly(Card[] attacker, int attackerIndex, Player opponent)
        {
#if CONSOLE
            Console.WriteLine("{0} attack {1} directly with {2}.", this.Name, opponent.Name, attacker[attackerIndex].Name);
#endif
            opponent.LifePoints -= attacker[attackerIndex].Attack;
#if CONSOLE
            Console.WriteLine("{0}'s life points are {1}.", opponent.Name, opponent.LifePoints);
#endif
        }

        public void attackMonster(Card[] attacker, int attackerIndex, Player opponent, Card[] defender, int defenderIndex)
        {
#if CONSOLE
            Console.WriteLine("{0} attacks {1}'s {2} with {3}.", this.Name, opponent.Name, defender[defenderIndex].Name, attacker[attackerIndex].Name);
#endif
            switch (defender[defenderIndex].Horizontal)
            {
                case false:
                    if (attacker[attackerIndex].Attack > defender[defenderIndex].Attack)
                    {
                        int damage = attacker[attackerIndex].Attack - defender[defenderIndex].Attack;
                        opponent.LifePoints -= damage;
#if CONSOLE
                        Console.WriteLine("{0} takes {1} points of damage.", opponent.Name, damage);
                        Console.WriteLine("{0}'s life points are {1}.", opponent.Name, opponent.LifePoints);
                        Console.WriteLine("{0} was destroyed.", defender[defenderIndex].Name);
#endif
                        opponent.move(opponent.MonsterZone, opponent.Graveyard, defender[defenderIndex], defenderIndex);
                    }
                    else if (attacker[attackerIndex].Attack == defender[defenderIndex].Attack)
                    {
#if CONSOLE
                        Console.WriteLine("Both monsters were destroyed.");
#endif
                        this.move(this.MonsterZone, this.Graveyard, attacker[attackerIndex], attackerIndex);
                        opponent.move(opponent.MonsterZone, opponent.Graveyard, defender[defenderIndex], defenderIndex);
                    }
                    else
                    {
                        int damage = defender[defenderIndex].Attack - attacker[attackerIndex].Attack;
                        this.LifePoints -= damage;
#if CONSOLE
                        Console.WriteLine("{0} takes {1} points of damage.", this.Name, damage);
                        Console.WriteLine("{0}'s life points are {1}.", this.Name, this.LifePoints);
                        Console.WriteLine("{0} was destroyed.", attacker[attackerIndex].Name);
#endif
                        this.move(this.MonsterZone, this.Graveyard, attacker[attackerIndex], attackerIndex);
                    }
                    break;
                case true:
                    if (attacker[attackerIndex].Attack > defender[defenderIndex].Defence)
                    {
#if CONSOLE
                        Console.WriteLine("{0} was destroyed.", defender[defenderIndex].Name);
#endif
                        opponent.move(opponent.MonsterZone, opponent.Graveyard, defender[defenderIndex], defenderIndex);
                    }
                    else if (attacker[attackerIndex].Attack == defender[defenderIndex].Defence)
                    {
#if CONSOLE
                        Console.WriteLine("{0} endured.", defender[defenderIndex].Name);
#endif
                    }
                    else
                    {
                        int damage = defender[defenderIndex].Defence - attacker[attackerIndex].Attack;
                        this.LifePoints -= damage;
#if CONSOLE
                        Console.WriteLine("{0} takes {1} points of damage.", this.Name, damage);
                        Console.WriteLine("{0}'s life points are {1}.", this.Name, this.LifePoints);
#endif
                    }
                    break;
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
