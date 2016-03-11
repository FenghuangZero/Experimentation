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

            Card[] trunk = new Card[40];
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            // XmlTextReader reader = new XmlTextReader("YGOCardDB.xml");

            
            /*
            foreach (MonsterCard m in deck)
            {
                deck[m] = new MonsterCard();
                deck[m].Name = XmlText
            }
            */

            trunk[0] = new Card();
            trunk[0].Name = "Tri-Horned Dragon";
            trunk[0].Description = "An unworthy dragon with three sharp horns sprouting from its head.";
            trunk[0].Number = 39111158;
            trunk[0].Type = "Dragon";
            trunk[0].Attribute = "Dark";
            trunk[0].Attack = 2850;
            trunk[0].Defence = 2350;
            trunk[0].Level = 8;
            //trunk[0].Name = "Red-Eyes Black Dragon";
            trunk[1] = new Card("Blue-Eyes White Dragon", "This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.", 89631139, "Dragon", "Light", 3000, 2500, 8);
            trunk[5] = new Card("Dark Magician", "The ultimate wizard in terms of attack and defense.", 46986414, "Spellcaster", "Dark", 2500, 2100, 7);

            player2.Deck[0] = trunk[1];

            player1.Deck[0] = trunk[5];

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
            player1.summon(player1.Hand[0]);
            player1.attackDirectly(player1.MonsterZone[0], player2);
            player2.draw();
            player2.summon(player2.Hand[0]);
            player2.attackMonster(player2.MonsterZone[0], player1, player1.MonsterZone[0]);
            Console.WriteLine("Player 1 has drawn Exodia. Player 1 Wins.");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
