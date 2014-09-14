using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.Entities
{
    public class Player
    {
        public decimal Balance { get; private set; }
        public decimal Bet { get; private set; }
        public Hand Hand { get; private set; }
        public Shoe Shoe { get; set; }

        public Player() : this(-1)
        {

        }

        public Player(int initialBalance)
        {
            Balance = initialBalance;
            Hand = new Hand();
        }

        public Hand NewHand()
        {
            Hand = new Hand();
            return Hand;
        }

        /// <summary>
        /// Increases the bet amount each time a bet is added to the hand.  Invoked through the betting coins in the BlackJackForm.cs UI
        /// </summary>
        /// <param name="amt"></param>
        public void IncreaseBet(decimal amt)
        {
            // Check to see if the user has enough money to make this bet
            if ((Balance - (Bet + amt)) >= 0)
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
            if ((Balance - Bet) >= 0)
            {
                Balance = Balance - Bet;
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
            if (Hand.GetSumOfHand() == 21)
                return true;
            else return false;
        }

        /// <summary>
        /// Check if the current player has bust
        /// </summary>
        /// <returns>returns true if the current player has bust</returns>
        public bool HasBust()
        {
            if (Hand.GetSumOfHand() > 21)
                return true;
            else return false;
        }

        /// <summary>
        /// Player has hit, draw a card from the deck and add it to the player's hand
        /// </summary>
        public void Hit()
        {
            Card c = Shoe.Draw();
            Hand.Cards.Add(c);
        }

        /// <summary>
        /// Player has chosen to double down, double the player's bet and hit once
        /// </summary>
        public void DoubleDown()
        {
            IncreaseBet(Bet);
            // Only decrease the balance by half of the current bet
            Balance = Balance - (Bet / 2);
            Hit();
        }

    }
}
