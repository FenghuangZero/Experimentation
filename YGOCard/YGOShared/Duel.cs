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
                if (p1.LifePoints <= 0 || p2.LifePoints <= 0)
                    break;
                turn(p1, p2);
                if (p1.LifePoints <= 0 || p2.LifePoints <= 0)
                    break;
                turn(p2, p1);
            }
            while (Turns < 20);
            if (p1.LifePoints <= 0)
                Debug.WriteLine("{0}'s life points are reduced to zero. {1} wins!", p1.Name, p2.Name);
            if (p2.LifePoints <= 0)
                Debug.WriteLine("{0}'s life points are reduced to zero. {1} wins!", p2.Name, p1.Name);
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
            var d = new DecisionMaking(p,o);
            p.canSummon = true;

            if (p.canSummon)
            {
                try { p.summon(p.Hand, d.bestAttacker()); }
                catch (NoMonsterinHandException e)
                {
                }
                catch (MonsterZoneFullException e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine("Only 5 monsters can be on the field at a time.");
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
