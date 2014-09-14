using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.Entities
{
    public class Player
    {
        public decimal Balance { get; set; }
        public int Losses { get; set; }
        public int Wins { get; set; }

        public List<Hand> Hands { get; private set; }
        public Shoe Shoe { get; set; }

        public Player(Shoe shoe) : this(shoe, -1)
        {
            Hands = new List<Hand>();
        }

        public Player(Shoe shoe, int initialBalance)
        {
            Balance = initialBalance;
        }

        public Hand NewHand()
        {
            var newHand = new Hand(this);
            Hands = new List<Hand>();
            Hands.Add(newHand);

            return newHand;
        }

    }
}
