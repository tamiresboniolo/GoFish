//Author: Tamires Boniolo

using System;
using System.Collections.Generic;

namespace GoFish
{
    public class RandomPlayer : Player
    {
        private static Random randomPlayerChoose = new Random();

        public RandomPlayer(string name) : base(name + "(Rnd)")
        { }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            //Choose player randomly
            while (true)
            {
                Player player = players[randomPlayerChoose.Next(0, players.Count)];

                if (player != this)
                {
                    return player;
                }
            }
        }
        public override Rank ChooseRankToAskFor()
        {
            //getting the first card in the hand and return the rank
            //0 beteween hand size
            return Hand.GetCards()[randomPlayerChoose.Next(0, Hand.Size())].Rank;
        }

    }
}


