#define CONSOLE
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGOShared;
using System.Resources;

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
            // cardDB.downloadCardList(13301001);
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
            var r = new ResourceManager("Resources", typeof(Program).Assembly);
            p.demo();
            
            // Keep the console window open in debug mode.
            Debug.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
