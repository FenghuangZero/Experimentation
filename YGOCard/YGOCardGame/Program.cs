using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace YGOCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            MonsterCard[] trunk = new MonsterCard[40];
            Player player1 = new Player();
            Player player2 = new Player();

            // XmlTextReader reader = new XmlTextReader("YGOCardDB.xml");

            
            /*
            foreach (MonsterCard m in deck)
            {
                deck[m] = new MonsterCard();
                deck[m].Name = XmlText
            }
            */

            trunk[0] = new MonsterCard();
            trunk[0].Name = "Blue-Eyes White Dragon";
            trunk[0].Description = "This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.";
            trunk[0].Number = 89631139;
            trunk[0].Type = "Dragon";
            trunk[0].Attribute = "Light";
            trunk[0].Attack = 3000;
            trunk[0].Defence = 2500;
            trunk[0].Level = 8;
            //deck[0].Name = "Red-Eyes Black Dragon";
            trunk[1] = new MonsterCard("Dark Magician", "The ultimate wizard in terms of attack and defense.", 46986414, "Spellcaster", "Dark", 2500, 2100, 7);

            player2.Deck[0] = trunk[0];
            player2.Deck[1] = trunk[0];
            player2.Deck[2] = trunk[0];

            player1.Deck[0] = trunk[1];

            // Describe cards in deck.
            /*
            foreach (MonsterCard m in deck)
            {
                Console.WriteLine(m.Name);
                Console.WriteLine(m.Description);
                Console.WriteLine("Attack: " + m.Attack);
                Console.WriteLine("Defence: " + m.Defence);
            }
            */

            //Mock Duel
            player1.draw();
            Console.WriteLine("Player 1 drew " + player1.Hand[0]);
            player1.summon(player1.Hand[0]);
            Console.WriteLine("Player 1 summons " + player1.MonsterZone[0].Name);
            Console.WriteLine("Player 1 attacks player 2 with " + player1.MonsterZone[0].Name);
            player2.LifePoints -= player1.MonsterZone[0].Attack;
            Console.WriteLine("Player 2 has " + player2.LifePoints + " life points.");
            player2.summon(trunk[0]);
            Console.WriteLine("Player 2 summons " + player2.MonsterZone[0].Name);
            Console.WriteLine("Player 2 attacks player 1's " + player1.MonsterZone[0].Name + " with " + player2.MonsterZone[0].Name);
            player1.LifePoints -= (player2.MonsterZone[0].Attack - player1.MonsterZone[0].Attack);
            Console.WriteLine("Player 1 has " + player1.LifePoints + " life points.");
            Console.WriteLine(player1.MonsterZone[0].Name + " is destroyed.");
            player1.MonsterZone[0] = null;
            player2.summon(trunk[0]);
            player2.summon(trunk[0]);
            Console.WriteLine("Player 2 has summoned two " + player2.MonsterZone[1].Name + "s.");
            Console.WriteLine("Player 1 has drawn Exodia. Player 1 Wins.");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
