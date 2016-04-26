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
using System.Reflection;
using System.IO;

namespace YGOCardGame
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Program class.
    /// </summary>
    class Program
    {
        List<Card> Trunk;
        XmlHandler cardDB;

        public List<Card> trunk
        {
            get
            {
                return Trunk;
            }

            set
            {
                Trunk = value;
            }
        }

        public async void demo()
        {
            //trunk = new List<Card>();
            cardDB = new XmlHandler();
            //await cardDB.loadXml(trunk);
            //var gameOn = new Duel(trunk);
            trunk = new List<Card>();
            trunk = await cardDB.downloadtoList(4007, 4107);
            foreach (Card c in trunk)
                Debug.WriteLine(c.Name);
            cardDB.writeXml(trunk);
        }

        /// <summary>
        /// Initializes the program. This is the first line of authored code executed.
        /// </summary>
        static void Main()
        {
            var p = new Program();
            var d = new Debug();
            p.demo();
            
            // Keep the console window open in debug mode.
            Debug.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
