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
        private static DBHandler cardDB = new DBHandler();
        List<Card> Trunk = new List<Card>(cardDB.loadXml());
        public List<Card> trunk { get { return Trunk; } set { Trunk = value; } }
        
        public void demo()
        {
            var gameOn = new Duel(trunk);
            /*trunk = await cardDB.downloadtoList(trunk, 4000, 5200);
            trunk = trunk.OrderBy(c => c.ID).ToList();
            // foreach (Card c in trunk)
                // Debug.WriteLine(c.Name);
            cardDB.writeXml(trunk); */
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
