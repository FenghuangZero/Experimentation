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
            p2.MainDeck.Add(t[4007]);
            p2.MainDeck.Add(t[4009]);
            p2.MainDeck.Add(t[4011]);
            p2.MainDeck.Add(t[4029]);
            p2.MainDeck.Add(t[4032]);
            p2.MainDeck.Add(t[4037]);
            d.emptyRecipie();
            p1.Deck = d.loadDeck(t, YGOConsole.Properties.Resources.STARTER_DECK_YUGI);
            p1.MainDeck.Add(t[4008]);
            p1.MainDeck.Add(t[4012]);
            p1.MainDeck.Add(t[4013]);
            p1.MainDeck.Add(t[4028]);
            p1.MainDeck.Add(t[4033]);
            p1.MainDeck.Add(t[4041]);
#endif
            mockDuel();
        }

        /// <summary>
        /// Performs an example duel for test purposes.
        /// </summary>
        public void mockDuel()
        {
            p1.MainDeck.Shuffle();
            p2.MainDeck.Shuffle();

            Queue<Card> p1Deck = new Queue<Card>();
            for (int i = 0; i < p1.MainDeck.Count; i++)
                p1Deck.Enqueue(p1.MainDeck.ElementAt(i));

            Queue<Card> p2Deck = new Queue<Card>();
            for (int i = 0; i < p2.MainDeck.Count; i++)
                p2Deck.Enqueue(p2.MainDeck.ElementAt(i));

            Debug.WriteLine(p1Deck.Dequeue().Name);
            Debug.WriteLine(p1Deck.Dequeue().Name);
            Debug.WriteLine(p1Deck.Dequeue().Name);
            Debug.WriteLine(p1Deck.Dequeue().Name);
            Debug.WriteLine(p1Deck.Dequeue().Name);
            Debug.WriteLine(p2Deck.Dequeue().Name);
            Debug.WriteLine(p2Deck.Dequeue().Name);
            Debug.WriteLine(p2Deck.Dequeue().Name);
            Debug.WriteLine(p2Deck.Dequeue().Name);
            Debug.WriteLine(p2Deck.Dequeue().Name);
            /*
            for (var i = 0; i < 6; i++)
                p1.draw();
            for (var i = 0; i < 6; i++)
                p2.draw();
            p1.draw();
            p1.set(p1.Hand, 0);
            p2.draw();
            p2.summon(p2.Hand, 1);
            p2.attackMonster(p2.MonsterZone, 0, p1, p1.MonsterZone, 0);*/
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
