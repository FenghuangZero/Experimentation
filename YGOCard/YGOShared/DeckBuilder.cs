using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Resources;

namespace YGOShared
{
    class DeckBuilder
    {
        private int?[][] Recipie = new int?[3][]
        {
            new int?[60],
            new int?[15],
            new int?[15]
        };
        
        public void addCardtoDeck(Card c)
        {
            int i = Array.IndexOf(Recipie[0], null);
            Recipie[0][i] = c.ID;
        }

        public void addCardtoExtra(Card c)
        {
            int i = Array.IndexOf(Recipie[1], null);
            Recipie[1][i] = c.ID;
        }

        public void addCardtoSide(Card c)
        {
            int i = Array.IndexOf(Recipie[2], null);
            Recipie[1][i] = c.ID;
        }

        public void removeCardfromDeck(Card c)
        {
            int i = Array.IndexOf(Recipie[0], c.ID);
            Recipie[0][i] = null;
        }

        public void removeCardfromExtra(Card c)
        {
            int i = Array.IndexOf(Recipie[1], c.ID);
            Recipie[0][i] = null;
        }

        public void removeCardfromSide(Card c)
        {
            int i = Array.IndexOf(Recipie[2], c.ID);
            Recipie[0][i] = null;
        }

        public void emptyRecipie()
        {
            Array.Clear(Recipie[0], 0, Recipie[0].Length);
            Array.Clear(Recipie[1], 0, Recipie[1].Length);
            Array.Clear(Recipie[2], 0, Recipie[1].Length);
        }

        public List<Card> loadDeck(List<Card> t, string resource)
        {
            
            var read = (resource).Split(',');
            List<Card> deck = new List<Card>();
            foreach (var s in read)
            {
                if (s != "" && s != null)
                    deck.Add(t[int.Parse(s)]);
            }
            return deck;
        }


        public void saveRecipie(string name)
        {
            name = name + ".txt";
            var recipie = new FileStream(name, FileMode.Create);
            var writer = new StreamWriter(recipie);

            foreach (var i in Recipie[0])
            {
                if (i != null)
                {
                    writer.Write(i.Value);
                    writer.Write(',');
                }
            }
            writer.WriteLine();
            foreach (var i in Recipie[1])
            {
                if (i != null)
                {
                    writer.Write(i.Value);
                    writer.Write(',');
                }
            }
            writer.WriteLine();
            foreach (var i in Recipie[2])
            {
                if (i != null)
                {
                    writer.Write(i.Value);
                    writer.Write(',');
                }
            }
            writer.Flush();
        }

        public void loadDeck(Player p, Card[] t)
        {
            for (var i = 0; i < 60; i++)
            {
                if (Recipie[0][i] != null)
                    p.MainDeck.Enqueue(t[Recipie[0][i].Value]);
            }
            for (var i = 0; i < 15; i++)
            {
                if (Recipie[1][i] != null)
                    p.ExtraDeck.Add(t[Recipie[1][i].Value]);
            }
            for (var i = 0; i < 15; i++)
            {
                if (Recipie[2][i] != null)
                    p.SideDeck.Add(t[Recipie[2][i].Value]);
            }
        }

        public DeckBuilder()
        { }
    }
}
