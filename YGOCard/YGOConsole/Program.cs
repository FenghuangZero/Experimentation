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
    /// Provides application-specific behavior to supplement the default Program class. This is primariliy for feature testing in a linear fashion before
    /// implementation of a graphical user interface. Creates a list of Card objects from an Xml file, to be used throughout the program.
    /// </summary>
    class Program
    {
        private static DBHandler cardDB = new DBHandler();
        List<Card> Trunk = new List<Card>(cardDB.loadXml());
        public List<Card> trunk { get { return Trunk; } }
        
        /// <summary>
        /// Runs a series of test methods.
        /// </summary>
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
        /// Initializes the program with a windows console.
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
