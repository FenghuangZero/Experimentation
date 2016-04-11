using System;
using System.Collections.Generic;
using System.Text;

namespace YGOShared
{
    class DeckBuilder
    {
        private int?[][] Recipie = new int?[2][]
        {
            new int?[60],
            new int?[15]
        };
        
        void addCard(Card c)
        {
            int i = Array.IndexOf(Recipie[0], null);
            Recipie[0][i] = c.ID;
        }

        void removeCard(Card c)
        {
            int i = Array.IndexOf(Recipie[0], c.ID);
            Recipie[0][i] = null;
        }

        void emptyRecipie()
        {
            Array.Clear(Recipie[0], 0, Recipie[0].Length);
            Array.Clear(Recipie[1], 0, Recipie[1].Length);
        }

        public DeckBuilder()
        { }
    }
}
