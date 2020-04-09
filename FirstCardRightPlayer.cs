//Author: Tamires Boniolo

using System;
using System.Collections.Generic;


namespace GoFish
{
    class FirstCardRightPlayer : Player
    {

        public FirstCardRightPlayer(string name) : base(name)
        { }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            if (players == null)
            {
                throw new Exception("Need a valid list of players");
            }

            //choose the first player on their right
            int i = 0;

            while (i <= players.Count)
            {
                if (this == players[i])
                {
                    if (i == 3)
                    {
                        return players[0];
                    }
                    else
                    {
                        return players[i + 1];
                    }
                }
                i++;
            }
            return null;
        }
        public override Rank ChooseRankToAskFor()
        {
            //getting the first card in the hand and return the rank
            return Hand.GetCards()[0].Rank;
        }
    }
}

