namespace Omegaluz.Blackjack.Entities
{
    public class Game
    {
        public Game(int initialBalance, int numberOfDecks = 6)
        {
            Shoe = new Shoe(numberOfDecks);
            Dealer = new Player(this, Shoe);
            Player = new Player(this, Shoe, initialBalance);
        }

        public Player Player { get; private set; }
        public Player Dealer { get; private set; }
        public Shoe Shoe { get; private set; }

        public void DealNewGame()
        {
            Player.NewHand();
            Dealer.NewHand();

            for (int i = 0; i < 2; i++)
            {
                var playerCard = Shoe.Draw();
                Player.Hands[0].Cards.Add(playerCard);

                var dealerCard = Shoe.Draw();
                if (i == 1)
                {
                    dealerCard.IsFaceUp = false;
                }
                Dealer.Hands[0].Cards.Add(dealerCard);
            }

            //Player.Shoe = Shoe;
            //Dealer.Shoe = Shoe;
        }

        /// <summary>
        ///     This method finishes playing the dealer's hand
        /// </summary>
        public void DealerPlay()
        {
            var dealerHand = Dealer.Hands[0];
            // Dealer's card that was facing down is turned up when they start playing
            dealerHand.Cards[1].IsFaceUp = true;

            // Check to see if dealer has a hand that is less than 17
            // If so, dealer should keep hitting until their hand is greater or equal to 17
            if (dealerHand.GetSumOfHand() < 17)
            {
                dealerHand.Hit();
                DealerPlay();
            }
        }

        /// <summary>
        ///     Get the game result.  This returns an EndResult value
        /// </summary>
        /// <returns></returns>
        public EndResult GetHandResult(Hand playerHand)
        {
            EndResult endState;
            var dealerHand = Dealer.Hands[0];

            // Check for blackjack
            if (playerHand.NumCards == 2 && playerHand.HasBlackJack() && dealerHand.NumCards == 2 &&
                dealerHand.HasBlackJack())
            {
                endState = EndResult.Push;
                playerHand.Push();
            }
            else if (playerHand.NumCards == 2 && playerHand.HasBlackJack())
            {
                endState = EndResult.PlayerBlackJack;
                playerHand.WinBlackjack();
            }
            else if (dealerHand.NumCards == 2 && dealerHand.HasBlackJack())
            {
                endState = EndResult.DealerBlackJack;
                playerHand.Lose();
            }
                // Check if the dealer has bust
            else if (dealerHand.HasBust())
            {
                endState = EndResult.DealerBust;
                playerHand.Win();
            }
            else if (dealerHand.CompareFaceValue(playerHand) > 0)
            {
                //dealer wins
                endState = EndResult.DealerWin;
                playerHand.Lose();
            }
            else if (dealerHand.CompareFaceValue(playerHand) == 0)
            {
                // push
                endState = EndResult.Push;
                playerHand.Push();
            }
                // Check if the dealer has bust
            else if (playerHand.HasBust())
            {
                endState = EndResult.PlayerBust;
                playerHand.Lose();
            }
            else
            {
                // player wins
                endState = EndResult.PlayerWin;
                playerHand.Win();
            }
            return endState;
        }
    }
}