using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using YGOShared;
#if WINDOWS_UWP
using Windows.Storage;
#endif

namespace YGOShared
{
    /// <summary>
    /// Controls the flow of a duel.
    /// </summary>
    class Duel
    {
        Player p1 = new Player("Player 1");
        Player p2 = new Player("Player 2");

        /// <summary>
        /// Assigns decks to each player.
        /// </summary>
        /// <param name="t">The card database.</param>
        public async void loadDeck(List<Card> t)
        {
            var d = new DeckBuilder();
#if WINDOWS_UWP
            var cardlistDirectory = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\Card Lists");
            var starterDecks = await cardlistDirectory.GetFileAsync("Starter Decks.xml");
            var deckXml = XDocument.Load(await starterDecks.OpenStreamForReadAsync());
            var kaiba = deckXml.Descendants("STARTER_DECK_KAIBA");
            var yugi = deckXml.Descendants("STARTER_DECK_YUGI");
            List<Card> recipie1 = d.loadDeck(t, kaiba.Single().Value);
            recipie1.Shuffle();
            for (var i = 0; i < recipie1.Count; i++)
                p1.MainDeck.Enqueue(recipie1.ElementAt(i));
            List<Card> recipie2 = d.loadDeck(t, yugi.Single().Value);
            recipie2.Shuffle();
            for (var i = 0; i < recipie1.Count; i++)
                p2.MainDeck.Enqueue(recipie1.ElementAt(i));

#endif
#if CONSOLE
            List<Card> recipie1 = d.loadDeck(t, YGOConsole.Properties.Resources.STARTER_DECK_KAIBA);
            recipie1.Shuffle();
            for (var i = 0; i < recipie1.Count; i++)
                p1.MainDeck.Enqueue(recipie1.ElementAt(i));
            List<Card> recipie2 = d.loadDeck(t, YGOConsole.Properties.Resources.STARTER_DECK_KAIBA);
            recipie2.Shuffle();
            for (var i = 0; i < recipie1.Count; i++)
                p2.MainDeck.Enqueue(recipie1.ElementAt(i));
#endif
            mockDuel();
        }

        /// <summary>
        /// Performs an example duel for test purposes.
        /// </summary>
        public void mockDuel()
        {
            p2.draw(5);
            p1.draw(5);

            
            Debug.WriteLine("Player 1 has drawn Exodia. Player 1 Wins.");
        }
        
        /// <summary>
        /// Initializes the object.
        /// </summary>
        public Duel()
        { }

        /// <summary>
        /// Initializes the object using the card database and begins a duel.
        /// </summary>
        /// <param name="t"></param>
        public Duel(List<Card> t)
        {
            loadDeck(t);
        }
    }
}
