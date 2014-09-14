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
        static void Main(string[] args)
        {
            var game = new Game(100);
            game.DealNewGame();
            game.Player.Hands[0].Hit();
        }
    }
}
