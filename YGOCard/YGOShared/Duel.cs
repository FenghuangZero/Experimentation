using System;
using System.Collections.Generic;
using System.Text;

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
        public void loadDeck(Card[] t)
        {
            p2.Deck[0] = t[4007];
            p1.Deck[0] = t[4056];
        }

        /// <summary>
        /// Performs an example duel for test purposes.
        /// </summary>
        public void mockDuel()
        {
            p1.draw();
            p1.summon(p1.Hand, 0);
            p1.attackDirectly(p1.MonsterZone, 0, p2);
            p2.draw();
            p2.summon(p2.Hand, 0);
            p2.attackMonster(p2.MonsterZone, 0, p1, p1.MonsterZone, 0);
#if CONSOLE
            Console.WriteLine("Player 1 has drawn Exodia. Player 1 Wins.");
#endif
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
            mockDuel();
        }
    }
}
