//Author: Tamires Boniolo

using System;
using System.Collections.Generic;


namespace GoFish
{
    public class CheatingPlayer : Player
    {
        private static Random randomPlayerChoose = new Random();

        public CheatingPlayer(string name) : base(name)
        { }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            if (players == null)
            {
                throw new Exception("Need a valid list of players");
            }

            Player candidate = this;
            while ((candidate == this) || (candidate.Hand.Size() == 0))
                candidate = players[randomPlayerChoose.Next(players.Count)];
            return candidate;
        }

        public override Rank ChooseRankToAskFor()
        {
            List<Rank> ranks = new List<Rank>((Rank[])Enum.GetValues(typeof(Rank)));
            int randomIndex = randomPlayerChoose.Next(ranks.Count);

            return ranks[randomIndex];
        }
    }
}
