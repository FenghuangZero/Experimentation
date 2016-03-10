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
        public MonsterCard[] MonsterZone = new MonsterCard[5];
        public SpellCard[] SpellZone = new SpellCard[5];
        public MonsterCard[] Deck = new MonsterCard[40];
        public MonsterCard[] Hand = new MonsterCard[8];
        public MonsterCard[] Graveyard = new MonsterCard[40];

        public void draw()
        {
            for (int i = 0; i < 8;)
            {
                if (Hand[i] == null)
                {
                    Hand[i] = Deck[0];

                    /*for (int j = 0; j < Deck.Length; j++)
                    {
                        if (j < Deck.Length)
                        {
                            Deck[j] = Deck[j + 1];
                        }
                        else
                            Deck[j] = null;
                    }*/
                }
                else
                    i++;
            }
        }

        public void summon(MonsterCard monster)
        {
            for (int i = 0; i < 5;)
            {
                if (MonsterZone[i] == null)
                {
                    MonsterZone[i] = monster;
                    break;
                }
                else
                    i++;
                
            }
        }

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
    }

}
