using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.Entities
{
    public class Card
    {
        public string Name
        {
            get
            {
                return string.Format("{0} of {1}", Rank, Suit);
            }
        }
        public Suit Suit { get; set; }
        public byte[] Image { get; set; }
        public Rank Rank { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
