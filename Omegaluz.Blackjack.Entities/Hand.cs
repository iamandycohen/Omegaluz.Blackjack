using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.Entities
{
    public class Hand
    {

        protected List<Card> cards = new List<Card>();
        public int NumCards { get { return cards.Count; } }
        public List<Card> Cards { get { return cards; } }

        /// <summary>
        /// This method compares two BlackJack hands
        /// </summary>
        /// <param name="otherHand"></param>
        /// <returns></returns>
        public int CompareFaceValue(object otherHand)
        {
            Hand aHand = otherHand as Hand;
            if (aHand != null)
            {
                return this.GetSumOfHand().CompareTo(aHand.GetSumOfHand());
            }
            else
            {
                throw new ArgumentException("Argument is not a Hand");
            }
        }

        /// <summary>
        /// Gets the total value of a hand from BlackJack values
        /// </summary>
        /// <returns>int</returns>
        public int GetSumOfHand()
        {
            int sum = 0;
            int numAces = 0;

            foreach (var card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    numAces++;
                    sum += 11;
                }
                else if (card.Rank == Rank.Jack || card.Rank == Rank.Queen || card.Rank == Rank.King)
                {
                    sum += 10;
                }
                else
                {
                    sum += (int)card.Rank;
                }
            }

            while (sum > 21 && numAces > 0)
            {
                sum -= 10;
                numAces--;
            }

            return sum;
        }

    }
}
