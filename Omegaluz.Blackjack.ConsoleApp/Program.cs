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
            var shoe = new Shoe();
            foreach (var card in shoe.Cards)
            {
                Console.WriteLine(card);
            }
            Console.ReadKey();
        }
    }
}
