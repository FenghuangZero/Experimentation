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

            deck[0] = new MonsterCard();
            
            //deck[0].setName("Blue-Eyes White Dragon");
            deck[0].setDesc("This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.");
            deck[0].setNum(89631139);
            deck[0].setType("Dragon");
            deck[0].setAttribute("Light");
            deck[0].setAttack(3000);
            deck[0].setDefence(2500);
            deck[0].setLevel(8);
            //deck[0].setName("Red-Eyes Black Dragon");
            deck[1] = new MonsterCard("Dark Magician", "The ultimate wizard in terms of attack and defense.", 46986414, "Spellcaster", "Dark", 2500, 2100, 7);

            // Describe cards in deck.
            for (int i = 0; i < deck.Length; i++)
            {
                Console.WriteLine(deck[i].getName());
                Console.WriteLine(deck[i].getDesc());
                Console.WriteLine("Attack: " + deck[i].getAttack());
                Console.WriteLine("Defence: " + deck[i].getDefence());
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
