//Author: Tamires Boniolo

using System;
using System.Collections.Generic;

namespace GoFish
{
    public class LastCardRandomPlayer : Player
    {
        private static Random randomPlayerChoose = new Random();

        public LastCardRandomPlayer(string name) : base(name)
        { }
        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            if (players == null)
            {
                throw new Exception("Need a valid list of players");
            }

            // asks a random player
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
            //getting the last card in the hand and return the rank
            return Hand.GetCards()[Hand.GetCards().Count - 1].Rank;
        }
    }
}


