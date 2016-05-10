using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Resources;
using System.Linq;

namespace YGOShared
{
    /// <summary>
    /// Handles reading and writing card id lists for saving user decks.
    /// </summary>
    class DeckBuilder
    {
        private List<int> Recipie;
        
        /// <summary>
        /// Adds the id of a specific card to the card list.
        /// </summary>
        /// <param name="c">The card to be added.</param>
        public void addCardtoDeck(Card c)
        {
            Recipie.Add(c.id);
        }

        /// <summary>
        /// Removes one copy of the specified card from the card list.
        /// </summary>
        /// <param name="c">The card to be removed.</param>
        public void removeCardfromDeck(Card c)
        {
            Recipie.Remove(c.id);
        }

        /// <summary>
        /// Resets the card list.
        /// </summary>
        public void emptyRecipie()
        {
            Recipie.Clear();
        }

        /// <summary>
        /// Converts a string representation of a list into an integer list.
        /// </summary>
        /// <param name="resource"></param>
        public void readRecipie(string resource)
        {
            var read = (resource.Split(','));
            foreach (var s in read)
                if (s != "" && s != null)
                    Recipie.Add(int.Parse(s));
        }

        /// <summary>
        /// Converts a string representation of a list straight to a list of Cards.
        /// </summary>
        /// <param name="t">The masterlist of Cards loaded from the Xml database.</param>
        /// <param name="resource">The string to be converted.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Writes the integer card list as a string to a text file.
        /// </summary>
        /// <param name="name">The name of the .txt file to be written.</param>
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

        /// <summary>
        /// Saves the card list to a player's Deck.
        /// </summary>
        /// <param name="p">The player to use the deck of cards.</param>
        /// <param name="t">The masterlist of Cards loaded from the Xml database.</param>
        public void saveDeck(Player p, List<Card> t)
        {
            foreach (int i in Recipie)
            {
                var query = from c in t where c.id == i select c;
                var card = query.FirstOrDefault();
                if (card.cardType.Contains("Fusion") || card.cardType.Contains("Syncro") || card.cardType.Contains("Xyz"))
                    p.ExtraDeck.Add(card);
                else
                    p.MainDeck.Enqueue(card);
            }
        }

        /// <summary>
        /// Loads a card list from a string and then saves that list to a player's deck.
        /// </summary>
        /// <param name="p">The player to use the deck of cards.</param>
        /// <param name="t">The masterlist of Cards loaded from the Xml database.</param>
        /// <param name="resource">The string representation of the card list.</param>
        public void saveDeck(Player p, List<Card> t, string resource)
        {
            readRecipie(resource);
            saveDeck(p, t);
            emptyRecipie();
        }

        /// <summary>
        /// Initialises the class.
        /// </summary>
        public DeckBuilder()
        {
            Recipie = new List<int>();
        }
    }
}
