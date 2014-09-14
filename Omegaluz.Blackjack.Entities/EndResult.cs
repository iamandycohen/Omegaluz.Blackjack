using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegaluz.Blackjack.Entities
{
    /// <summary>
    /// EndResult maintains the game result state
    /// </summary>
    public enum EndResult
    {
        DealerBlackJack, PlayerBlackJack, PlayerBust, DealerBust, Push, PlayerWin, DealerWin
    }
}
