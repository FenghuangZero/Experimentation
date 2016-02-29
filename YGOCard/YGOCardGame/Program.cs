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

            Card blueEyes = new Card("Blue-Eyes White Dragon", "This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.", 89631139, "Dragon");


            Console.WriteLine(blueEyes.getName());
            Console.WriteLine(blueEyes.getDesc());
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
