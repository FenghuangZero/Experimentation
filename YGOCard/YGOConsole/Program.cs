using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGOShared;

namespace YGOCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World");

            Card[] trunk = new Card[130];
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            XmlHandler cardDB = new XmlHandler();
            
            cardDB.loadXml(trunk);
            cardDB.downloadDB();

            player2.Deck[0] = trunk[1];
            player1.Deck[0] = trunk[5];

            //Mock Duel
            player1.draw();
            player1.summon(player1.Hand, 0);
            player1.attackDirectly(player1.MonsterZone, 0, player2);
            player2.draw();
            player2.summon(player2.Hand, 0);
            player2.attackMonster(player2.MonsterZone, 0, player1, player1.MonsterZone, 0);
            Console.WriteLine("Player 1 has drawn Exodia. Player 1 Wins.");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
