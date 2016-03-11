using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            XDocument doc = XDocument.Load("YGOCardDB.xml");

            var dbName = doc.Descendants("Name");
            var dbDescription = doc.Descendants("Description");
            var dbNumber = doc.Descendants("Number");
            var dbType = doc.Descendants("Type");
            var dbAttribute = doc.Descendants("Attribute");
            var dbAttack = doc.Descendants("Attack");
            var dbDefence = doc.Descendants("Defence");
            var dbLevel = doc.Descendants("Level");

            // Load cards from XML
            for (int i = 0; i < 40; i++)
            {
                trunk[i] = new Card();
                trunk[i].Name = dbName.ElementAt(i).Value;
                trunk[i].Description = dbDescription.ElementAt(i).Value;
                trunk[i].Number = Int32.Parse(dbNumber.ElementAt(i).Value);
                trunk[i].Type = dbType.ElementAt(i).Value;
                trunk[i].Attribute = dbAttribute.ElementAt(i).Value;
                trunk[i].Attack = Int32.Parse(dbAttack.ElementAt(i).Value);
                trunk[i].Defence = Int32.Parse(dbDefence.ElementAt(i).Value);
                trunk[i].Level = Int32.Parse(dbLevel.ElementAt(i).Value);
            }

            player2.Deck[0] = trunk[1];
            player1.Deck[0] = trunk[5];

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
