using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Resources;
using System.Linq;

namespace YGOShared
{
    class DeckBuilder
    {
        private List<int> Recipie;
        
        public void addCardtoDeck(Card c)
        {
            Recipie.Add(c.ID);
        }

        public void removeCardfromDeck(Card c)
        {
            Recipie.Remove(c.ID);
        }

        public void emptyRecipie()
        {
            Recipie.Clear();
        }

        public void readRecipie(string resource)
        {
            var read = (resource.Split(','));
            foreach (var s in read)
                if (s != "" && s != null)
                    Recipie.Add(int.Parse(s));
        }

        public List<Card> loadDeck(List<Card> t, string resource)
        {
            
            var read = (resource).Split(',');
            var deck = new List<Card>();
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

            foreach (var i in Recipie)
            {
                writer.Write(i);
                writer.Write(',');
            }
            
            writer.Flush();
        }

        public void saveDeck(Player p, List<Card> t)
        {
            foreach (int i in Recipie)
            {
                var query = from c in t where c.ID == i select c;
                var card = query.FirstOrDefault();
                if (card.CardType.Contains("Fusion") || card.CardType.Contains("Syncro") || card.CardType.Contains("Xyz"))
                    p.ExtraDeck.Add(card);
                else
                    p.MainDeck.Enqueue(card);
            }
        }

        public void saveDeck(Player p, List<Card> t, string resource)
        {
            readRecipie(resource);
            saveDeck(p, t);
            emptyRecipie();
        }

        public DeckBuilder()
        {
            Recipie = new List<int>();
        }
    }
}
