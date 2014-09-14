using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.Entities
{
    public class Shoe
    {
        public List<Card> Cards { get; private set; }
        public List<Card> UsedCards { get; private set; }

        public Shoe(int numberOfDecks = 6)
        {
            Cards = new List<Card>();
            UsedCards = new List<Card>();

            for (int i = 1; i <= numberOfDecks; i++)
            {
                var deckOfCards = new Deck().Cards;
                Cards.AddRange(deckOfCards);
            }

            ShuffleShoe();
        }

        private void ShuffleShoe()
        {
            var rand = new Random();
            var shuffledCards = Cards
                .OrderBy(c => rand.Next())
                .ToList();

            Cards = shuffledCards;
        }

        /// <summary>
        /// Draws one card and removes it from the deck
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            var card = Cards[0];
            Cards.RemoveAt(0);
            UsedCards.Add(card);

            return card;
        }

        public void RecycleUsedCards()
        {
            var rand = new Random();
            var shuffledUsedCards = UsedCards
                .OrderBy(c => rand.Next())
                .ToList();

            UsedCards.Clear();
            Cards.AddRange(shuffledUsedCards);
        }

    }
}
