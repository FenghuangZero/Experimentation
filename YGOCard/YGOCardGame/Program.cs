using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGOCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            MonsterCard[] deck = new MonsterCard[2];
            Player player1 = new Player();
            Player player2 = new Player();

            deck[0] = new MonsterCard();
            deck[0].Name = "Blue-Eyes White Dragon";
            deck[0].Description = "This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.";
            deck[0].Number = 89631139;
            deck[0].Type = "Dragon";
            deck[0].Attribute = "Light";
            deck[0].Attack = 3000;
            deck[0].Defence = 2500;
            deck[0].Level = 8;
            //deck[0].Name = "Red-Eyes Black Dragon";
            deck[1] = new MonsterCard("Dark Magician", "The ultimate wizard in terms of attack and defense.", 46986414, "Spellcaster", "Dark", 2500, 2100, 7);

            // Describe cards in deck.
            /*
            for (int i = 0; i < deck.Length; i++)
            {
                Console.WriteLine(deck[i].Name);
                Console.WriteLine(deck[i].Description);
                Console.WriteLine("Attack: " + deck[i].Attack);
                Console.WriteLine("Defence: " + deck[i].Defence);
            }
            */

            //Mock Duel
            Console.WriteLine("Player 1 summons " + deck[1].Name);
            Console.WriteLine("Player 1 attacks player 2 with " + deck[1].Name);
            player2.LifePoints -= deck[1].Attack;
            Console.WriteLine("Player 2 has " + player2.LifePoints + " life points.");
            Console.WriteLine("Player 2 summons " + deck[0].Name);
            Console.WriteLine("Player 2 attacks player 1's " + deck[1].Name + " with " + deck[0].Name);
            player1.LifePoints -= (deck[0].Attack - deck[1].Attack);
            Console.WriteLine("Player 1 has " + player1.LifePoints);
            Console.WriteLine(deck[1].Name + " is destroyed.");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
