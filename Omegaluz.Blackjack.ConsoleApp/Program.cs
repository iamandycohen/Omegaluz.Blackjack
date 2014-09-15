using Omegaluz.Blackjack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.ConsoleApp
{
    class Program
    {
        private static int gameCount = 0;

        static void Main(string[] args)
        {
            var game = new Game(100);
            PlayGame(game);
        }

        private static void PlayGame(Game game)
        {
            gameCount++;
            game.DealNewGame();

            var amount = GetBetAmount();

            var dealerHand = game.Dealer.Hands[0];
            DisplayHand(dealerHand, "Dealer");

            PlayPlayerHands(game, amount);

            game.DealerPlay();

            DisplayHand(dealerHand, "Dealer");

            Console.Write(" Total: {0}", dealerHand.GetSumOfHand());
            Console.WriteLine();

            DisplayResults(game);

            if (gameCount == 4)
            {
                game.Shoe.RecycleUsedCards();
                gameCount = 0;
            }

            Console.WriteLine("Wins: {0} Losses: {1} Percentage: {2}", game.Player.Wins, game.Player.Losses, (decimal)game.Player.Wins / ((decimal)game.Player.Wins + (decimal)game.Player.Losses));

            Console.WriteLine("Play again?");
            var playKey = Console.ReadKey();
            Console.WriteLine();
            switch (playKey.Key)
            {
                case ConsoleKey.Y:
                    Console.WriteLine();
                    PlayGame(game);
                    break;
            }
        }

        private static void DisplayResults(Game game)
        {
            foreach (var playerHand in game.Player.Hands)
            {
                DisplayHand(playerHand, "Player");
                Console.WriteLine();

                Console.WriteLine(game.GetHandResult(playerHand));
            }

            Console.WriteLine();
            Console.WriteLine("Balance: {0}", game.Player.Balance);

            Console.WriteLine();
        }

        private static void PlayPlayerHands(Game game, int amount)
        {
            var firstHand = true;
            foreach (var playerHand in game.Player.Hands)
            {
                if (firstHand)
                {
                    playerHand.IncreaseBet(amount);
                    playerHand.PlaceBet();
                }

                PlayHand(playerHand);
                firstHand = false;
            }
        }

        private static int GetBetAmount()
        {
            Console.WriteLine("Bet Amount?");
            var amountString = Console.ReadLine();
            var amount = int.Parse(amountString ?? string.Empty);
            Console.WriteLine();
            return amount;
        }

        private static void PlayHand(Hand playerHand)
        {
            Console.WriteLine();
            DisplayHand(playerHand, "Player");
            Console.WriteLine();

            Console.WriteLine("[H]it [S]tand [D]ouble-Down s[P]lit");
            GetPlayerInput(playerHand);
        }

        private static void DisplayHand(Hand hand, string name)
        {
            var handString = string.Format("{0} Hand: {1}", name, string.Join(" ", hand.Cards.Select(CardString)));
            var betString = string.Empty;
            var totalString = string.Empty;

            if (hand.Bet > 0)
            {
                betString = string.Format("Bet: {0}", hand.Bet);
                totalString = string.Format("Total: {0}", hand.GetSumOfHand());
            }

            Console.WriteLine("{0} {1} {2}", handString, betString, totalString);
        }

        private static void GetPlayerInput(Hand hand)
        {
            try
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.H:
                        hand.Hit();
                        if (!hand.HasBust())
                            PlayHand(hand);
                        break;
                    case ConsoleKey.S:
                        break;
                    case ConsoleKey.D:
                        hand.DoubleDown();
                        if (!hand.HasBust())
                            PlayHand(hand);
                        break;
                    case ConsoleKey.P:
                        hand.Split();
                        PlayHand(hand);
                        break;
                    case ConsoleKey.Escape:
                        break;
                }

                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                GetPlayerInput(hand);
            }
        }

        private static string CardString(Card card)
        {
            return string.Format("[ {0} ]", !card.IsFaceUp ? "XXXXXXXXX" : card.ToString());
        }
    }
}
