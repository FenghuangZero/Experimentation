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

        public void summon(MonsterCard monster)
        {
            for (int i = 0; i < 5;)
            {
                if (MonsterZone[i] == null)
                {
                    MonsterZone[i] = monster;
                    break;
                }
                
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
