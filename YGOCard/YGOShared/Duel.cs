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
        Player p1 = new Player("Kaiba");
        Player p2 = new Player("Yugi");
        int Turns = 0;
        string Phase = "";

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

#elif CONSOLE
            d.saveDeck(p1, t, YGOCardGame.Properties.Resources.STARTER_DECK_KAIBA);
            d.saveDeck(p2, t, YGOCardGame.Properties.Resources.STARTER_DECK_YUGI);
            p1.MainDeck.Shuffle();
            p2.MainDeck.Shuffle();
#endif
            mockDuel();
        }

        /// <summary>
        /// Performs an example duel for test purposes.
        /// </summary>
        public void mockDuel()
        {
            p1.draw(5);
            p2.draw(5);
            turn(p1, p2);
            turn(p2, p1);
            
            Debug.WriteLine("{0} has drawn Exodia.{0} Wins.", p2.Name);
        }
        
        public void turn(Player p, Player o)
        {
            Turns++;
            Debug.WriteLine("{0}'s turn.", p.Name);
            if (Turns > 1)
                p.canAttack = true;
            Phase = "Draw Phase";
            Debug.WriteLine(Phase);
            p.draw();
            Phase = "Standby Phase";
            Debug.WriteLine(Phase);
            Phase = "Main Phase 1";
            Debug.WriteLine(Phase);
            p.canSummon = true;
            var normalSummonable = p.Hand.Where(m => m.monsterType != "" && m.level < 5);
            var tributeSummonable = p.Hand.Where(m => m.monsterType != "" && m.level > 4);
            Debug.WriteLine("{0} can summon: ", p.Name);
            foreach (var m in normalSummonable)
                Debug.WriteLine(m.nameOnField);
            if (p.canAttack == true)
            {
                normalSummonable.OrderBy(m => m.atkOnField);
                p.summon(p.Hand, normalSummonable.First());
                p.canSummon = false;
            }
            else
            {
                normalSummonable.OrderBy(m => m.defOnField);
                p.set(p.Hand, normalSummonable.First());
                p.canSummon = false;
            }
            Phase = "Battle Phase";
            
            if (p.canAttack == true)
            {                
                foreach (var m in p.MonsterZone)
                {
                    var weakerAtkPosOpp = o.MonsterZone.Where(x => x.atkOnField < m.atkOnField && x.Horizontal == false);
                    var weakerDefPosOpp = o.MonsterZone.Where(x => x.defOnField < m.defOnField && x.Horizontal == true);
                    weakerAtkPosOpp.OrderBy(x => x.atkOnField);
                    weakerAtkPosOpp.OrderBy(x => x.defOnField);
                    if (weakerAtkPosOpp.Any())
                        p.attackMonster(m, weakerAtkPosOpp.First(), o);
                    else if (weakerDefPosOpp.Any())
                        p.attackMonster(m, weakerDefPosOpp.First(), o);
                    else if (o.MonsterZone.Any() == false)
                        p.attackPlayer(m, o);
                }
            }
            Phase = "Main Phase 2";
            Debug.WriteLine(Phase);
            Phase = "End Phase";
            Debug.WriteLine(Phase);
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
