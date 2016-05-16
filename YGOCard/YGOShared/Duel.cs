using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using YGOShared;
using System.Threading.Tasks;
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
        /// Assigns prebuilt decks to each player, for use in the AI demonstration.
        /// </summary>
        /// <param name="t">The masterlist of Cards loaded from the Xml database.</param>
        public async void loadDeck(List<Card> t)
        {
            var d = new DeckBuilder();
#if WINDOWS_UWP
            var cardlistDirectory = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\Card Lists");
            var starterDecks = await cardlistDirectory.GetFileAsync("Starter Decks.xml");
            var deckXml = XDocument.Load(await starterDecks.OpenStreamForReadAsync());
            var kaiba = deckXml.Descendants("STARTER_DECK_KAIBA");
            var yugi = deckXml.Descendants("STARTER_DECK_YUGI");
            d.saveDeck(p1, t, kaiba.Single().Value);
            d.saveDeck(p2, t, yugi.Single().Value);
            // List<Card> recipie1 = d.loadDeck(t, kaiba.Single().Value);
            // recipie1.Shuffle();
            // for (var i = 0; i < recipie1.Count; i++)
            //     p1.MainDeck.Enqueue(recipie1.ElementAt(i));
            //  List<Card> recipie2 = d.loadDeck(t, yugi.Single().Value);
            // recipie2.Shuffle();
            // for (var i = 0; i < recipie1.Count; i++)
            //     p2.MainDeck.Enqueue(recipie1.ElementAt(i));

#elif CONSOLE
            await Task.Delay(1);
            d.saveDeck(p1, t, YGOCardGame.Properties.Resources.STARTER_DECK_KAIBA);
            d.saveDeck(p2, t, YGOCardGame.Properties.Resources.STARTER_DECK_YUGI);
#endif
            p1.MainDeck.Shuffle();
            p2.MainDeck.Shuffle();
            mockDuel();
        }

        /// <summary>
        /// Performs an example duel for test purposes, using AI controlled players.
        /// </summary>
        public void mockDuel()
        {
            p1.draw(5);
            p2.draw(5);
            do
            {
                turn(p1, p2);
                turn(p2, p1);
            }
            while (Turns < 20);
            
            Debug.WriteLine("{0} has drawn Exodia.{0} Wins.", p2.Name);
        }
        
        /// <summary>
        /// Carries out a single turn.
        /// </summary>
        /// <param name="p">The player whose turn is currently in progress.</param>
        /// <param name="o">The opponent of the player who is currently taking their turn.</param>
        public void turn(Player p, Player o)
        {
            Turns++;
            Debug.WriteLine("{0}'s turn.", p.Name);
            if (Turns > 1)
                p.canAttack = true;
            drawPhase(p);
            standbyPhase(p);
            mainPhase1(p, o);
            battlePhase(p, o);
            mainPhase2(p);
            endPhase(p);
        }

        public void drawPhase(Player p)
        {
            Phase = "Draw Phase";
            Debug.WriteLine(Phase);
            if (p.canDraw)
                p.draw();
        }

        public void standbyPhase(Player p)
        {
            Phase = "Standby Phase";
            Debug.WriteLine(Phase);
        }

        public void mainPhase1(Player p, Player o)
        {
            Phase = "Main Phase 1";
            Debug.WriteLine(Phase);
            p.canSummon = true;
            var normalSummonable = p.Hand.Where(m => m.monsterType != "" && m.level < 5);
            var tributeSummonable = p.Hand.Where(m => m.monsterType != "" && m.level > 4);
            var oppsAtkPosMons = o.MonsterZone.Where(m => m.monsterType != "" && m.Horizontal == false);
            var oppsDefPosMons = o.MonsterZone.Where(m => m.monsterType != "" && m.Horizontal);
            Debug.WriteLine("{0} can summon: ", p.Name);
            foreach (var m in normalSummonable)
                Debug.WriteLine(m.nameOnField);
            if (p.MonsterZone.Count < 5 && p.canSummon)
            {
                if (oppsAtkPosMons.Any())
                {
                    oppsAtkPosMons.OrderBy(m => m.atkOnField);
                    normalSummonable.OrderBy(m => m.atkOnField);
                    if (oppsAtkPosMons.First().atkOnField < normalSummonable.First().atkOnField)
                        p.summon(p.Hand, normalSummonable.First());
                    else if (oppsAtkPosMons.First().atkOnField >= normalSummonable.First().atkOnField)
                    {
                        normalSummonable.OrderBy(m => m.defOnField);
                        if (normalSummonable.First().defOnField >= oppsAtkPosMons.First().atkOnField)
                            p.summon(p.Hand, normalSummonable.First());
                        else
                        {
                            if (normalSummonable.OrderBy(m => m.atkOnField).First().atkOnField > normalSummonable.OrderBy(m => m.defOnField).First().defOnField)
                            { }
                        }
                    }
                }
            }
        }

        public void battlePhase(Player p, Player o)
        {
            Phase = "Battle Phase";
            Debug.WriteLine(Phase);
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
        }

        public void mainPhase2(Player p)
        {
            Phase = "Main Phase 2";
            Debug.WriteLine(Phase);
        }

        public void endPhase(Player p)
        {
            Phase = "End Phase";
            Debug.WriteLine(Phase);
        }

        /// <summary>
        /// Initializes the class.
        /// </summary>
        public Duel()
        { }

        /// <summary>
        /// Initializes the class using the card database and begins a demonstration duel.
        /// </summary>
        /// <param name="t">The masterlist of Cards loaded from the Xml database.</param>
        public Duel(List<Card> t)
        {
            loadDeck(t);
        }
    }
}
