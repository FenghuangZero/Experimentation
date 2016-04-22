using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
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
        public async void loadDeck(Card[] t)
        {
            var d = new DeckBuilder();
#if WINDOWS_UWP
            var cardlistDirectory = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\Card Lists");
            var starterDecks = await cardlistDirectory.GetFileAsync("Starter Decks.xml");
            var deckXml = XDocument.Load(await starterDecks.OpenStreamForReadAsync());
            var kaiba = deckXml.Descendants("STARTER_DECK_KAIBA");
            var yugi = deckXml.Descendants("STARTER_DECK_YUGI");
            p2.Deck = d.loadDeck(t, kaiba.Single().Value);
            d.emptyRecipie();
            p1.Deck = d.loadDeck(t, yugi.Single().Value);

#endif
#if CONSOLE
            p2.Deck = d.loadDeck(t, YGOConsole.Properties.Resources.STARTER_DECK_KAIBA);
            d.emptyRecipie();
            p1.Deck = d.loadDeck(t, YGOConsole.Properties.Resources.STARTER_DECK_YUGI);
#endif
            mockDuel();
        }

        /// <summary>
        /// Performs an example duel for test purposes.
        /// </summary>
        public void mockDuel()
        {
            for (var i = 0; i < 6; i++)
                p1.draw();
            for (var i = 0; i < 6; i++)
                p2.draw();
            p1.draw();
            p1.set(p1.Hand, 0);
            p2.draw();
            p2.summon(p2.Hand, 1);
            p2.attackMonster(p2.MonsterZone, 0, p1, p1.MonsterZone, 0);
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
        public Duel(Card[] t)
        {
            loadDeck(t);
        }
    }
}
