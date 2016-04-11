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
        Card[] trunk;
        XmlHandler cardDB;

        public async void demo()
        {
            trunk = new Card[12273];
            cardDB = new XmlHandler();
            await cardDB.loadXml(trunk);
            Duel gameOn = new Duel(trunk);            
        }

        /// <summary>
        /// Initializes the program. This is the first line of authored code executed.
        /// </summary>
        static void Main()
        {
            //cardDB.downloadToArray();
            Program p = new Program();
            p.demo();
            
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
