using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.Entities
{
    public class Hand
    {

        public Shoe Shoe
        {
            get
            {
                return Player.Shoe;
            }
        }

        public Player Player { get; private set; }

        protected List<Card> cards = new List<Card>();
        public int NumCards { get { return cards.Count; } }
        public List<Card> Cards { get { return cards; } }
        public decimal Bet { get; private set; }

        /// <summary>
        /// This method compares two BlackJack hands
        /// </summary>
        /// <param name="otherHand"></param>
        /// <returns></returns>
        public int CompareFaceValue(object otherHand)
        {
            var aHand = otherHand as Hand;
            if (aHand != null)
            {
                return GetSumOfHand().CompareTo(aHand.GetSumOfHand());
            }
            throw new ArgumentException("Argument is not a Hand");
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

        public Hand(Player player)
        {
            Player = player;
        }

        /// <summary>
        /// Increases the bet amount each time a bet is added to the hand.  Invoked through the betting coins in the BlackJackForm.cs UI
        /// </summary>
        /// <param name="amt"></param>
        public void IncreaseBet(decimal amt)
        {
            // Check to see if the user has enough money to make this bet
            if ((Player.Balance - (Bet + amt)) >= 0)
            {
                // Add money to the bet
                Bet += amt;
            }
            else
            {
                throw new Exception("You do not have enough money to make this bet.");
            }
        }

        /// <summary>
        /// Places the bet and subtracts the amount from "My Account"
        /// </summary>
        public void PlaceBet()
        {
            // Check to see if the user has enough money to place this bet
            if ((Player.Balance - Bet) >= 0)
            {
                Player.Balance = Player.Balance - Bet;
            }
            else
            {
                throw new Exception("You do not have enough money to place this bet.");
            }
        }

        /// <summary>
        /// Set the bet value back to 0
        /// </summary>
        public void ClearBet()
        {
            Bet = 0;
        }

        /// <summary>
        /// Check if the current player has BlackJack
        /// </summary>
        /// <returns>Returns true if the current player has BlackJack</returns>
        public bool HasBlackJack()
        {
            if (GetSumOfHand() == 21)
                return true;
            return false;
        }

        /// <summary>
        /// Check if the current player has bust
        /// </summary>
        /// <returns>returns true if the current player has bust</returns>
        public bool HasBust()
        {
            if (GetSumOfHand() > 21)
                return true;
            return false;
        }

        /// <summary>
        /// Player has hit, draw a card from the deck and add it to the player's hand
        /// </summary>
        public void Hit()
        {
            Card c = Shoe.Draw();
            Cards.Add(c);
        }

        /// <summary>
        /// Player has chosen to double down, double the player's bet and hit once
        /// </summary>
        public void DoubleDown()
        {
            IncreaseBet(Bet);
            // Only decrease the balance by half of the current bet
            Player.Balance = Player.Balance - (Bet / 2);
            Hit();
        }

        public void Split()
        {
            var rank = Cards.First().Rank;
            if (rank == Rank.Jack || rank == Rank.Queen || rank == Rank.King)
            {
                rank = Rank.Ten;
            }

            if (Cards.Count == 2 && Cards.All(card => {
                var adjustedRank = card.Rank;

                if (card.Rank == Rank.Jack || card.Rank == Rank.Queen || card.Rank == Rank.King)
                {
                    adjustedRank = Rank.Ten;
                }

                return adjustedRank == rank;
            }))
            {
                var splitCard = Cards.First();
                var splitHand = new Hand(Player);

                splitHand.Cards.Add(splitCard);
                Player.Hands.Add(splitHand);

                Cards.Remove(splitCard);

                splitHand.IncreaseBet(Bet);
                Player.Balance = Player.Balance - (splitHand.Bet / 2);
            }
            else
            {
                throw new Exception("Cards must be of equal value to split.");
            }
        }

        /// <summary>
        /// Update the player's record with a loss
        /// </summary>
        public void Lose()
        {
            Player.Losses += 1;
        }

        /// <summary>
        /// Update the player's record with a blackjack win
        /// </summary>
        public void WinBlackjack()
        {
            Player.Balance += (Bet * 2) + (Bet / 2);
            Player.Wins += 1;
        }

        /// <summary>
        /// Update the player's record with a win
        /// </summary>
        public void Win()
        {
            Player.Balance += Bet * 2;
            Player.Wins += 1;
        }

        public void Push()
        {
            Player.Balance += Bet;
        }

    }
}
