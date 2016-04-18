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
            cardDB.downloadCardList(14401000);
            var gameOn = new Duel(trunk);
        }

        /// <summary>
        /// Initializes the program. This is the first line of authored code executed.
        /// </summary>
        static void Main()
        {
            //cardDB.downloadToArray();
            var p = new Program();
            var d = new Debug();
            p.demo();
            
            // Keep the console window open in debug mode.
            Debug.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
