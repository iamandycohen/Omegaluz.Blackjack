using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.Entities
{
    public class Deck
    {
        public List<Card> Cards { get; private set; }

        public Deck()
        {

            Cards = new List<Card>();

            for (int s = 1; s <= 4; s++)
            {
                var suit = (Suit)s;

                for (int r = 1; r <= 13; r++)
                {
                    var rank = (Rank)r;
                    var card = new Card
                    {
                        Rank = rank,
                        Suit = suit
                    };

                    Cards.Add(card);
                }
            }
        }
    }
}
