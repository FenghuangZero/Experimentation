#define CONSOLE
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
    /// <summary>
    /// Provides application-specific behavior to supplement the default Program class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Initializes the program. This is the first line of authored code executed.
        /// </summary>
        static void Main()
        {
            Card[] trunk = new Card[12273];
            XmlHandler cardDB = new XmlHandler();
           
            cardDB.downloadToArray();
            //cardDB.loadXml(trunk);

            //Duel gameOn = new Duel(trunk);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
