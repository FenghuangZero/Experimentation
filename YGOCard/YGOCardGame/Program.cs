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

            var syllable = "ha";
            var laugh = "";
            for (var i = 0; i < 10; i++)
            {
                laugh += syllable;
                Console.WriteLine(laugh);
            }
            foreach (var ch in laugh)
            {
                if (ch == 'h')
                    Console.Write("H");
                else
                    Console.Write(ch);
            }
            Console.WriteLine();
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
