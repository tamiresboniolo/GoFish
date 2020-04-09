//Author: Tamires Boniolo

using System;
using System.Collections.Generic;

namespace GoFish
{
    public class LastCardLeftPlayer : Player
    {
        public LastCardLeftPlayer(string name) : base(name)
        { }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            if (players == null)
            {
                throw new Exception("Need a valid list of players");
            }

            //choose the first player on their left
            int i = 0;

            while (i <= players.Count)
            {
                if (this == players[i])
                {
                    if (i == 0)
                    {
                        return players[3];
                    }
                    else
                    {
                        return players[i - 1];
                    }
                }
                i++;
            }
            return null;
        }

        public override Rank ChooseRankToAskFor()
        {
            //getting the last card in the hand and return the rank
            return Hand.GetCards()[Hand.GetCards().Count - 1].Rank;
        }
    }
}

