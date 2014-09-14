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

        public Shoe(int numberOfDecks = 6)
        {
            Cards = new List<Card>();

            for (int i = 1; i <= numberOfDecks; i++)
            {
                var deckOfCards = new Deck().Cards;
                Cards.AddRange(deckOfCards);
            }

            Shuffle();
        }

        public void Shuffle()
        {
            var rand = new Random();
            var shuffledCards = Cards
                .OrderBy(c => rand.Next())
                .ToList();

            Cards = shuffledCards;
        }
    }
}
