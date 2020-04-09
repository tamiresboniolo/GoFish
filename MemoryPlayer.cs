//Author: Tamires Boniolo

using System;
using System.Collections.Generic;

namespace GoFish
{
    public class MemoryPlayer : Player
    {
        private static Random randomPlayerChoose = new Random();
        private Player playerToAsk;
        private Rank rankToAsk;

        public MemoryPlayer(string name) : base(name)
        {
            playerToAsk = this;
            rankToAsk = Rank.Ace;
        }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            if (players == null)
            {
                throw new Exception("Need a valid list of players");
            }

            Player candidate = null;
            int tries = 0;
            int i = randomPlayerChoose.Next(players.Count);
            bool foundPlayer = false;
            while (!foundPlayer)
            {
                tries++;
                candidate = players[i++ % players.Count];
                if (candidate == this || candidate.Hand.Size() == 0)
                    continue;

                if (candidate.HasRankInHand(LastRankAsked))
                    foundPlayer = true;                 // Found player who recently asked for a card we have!

                if (tries > players.Count)
                {
                    foundPlayer = true;                 // Giving up.  Ask player for a random rank.
                    List<Card> cards = Hand.GetCards();
                    rankToAsk = cards[randomPlayerChoose.Next(Hand.Size())].Rank;
                }
            }
            playerToAsk = candidate;
            return playerToAsk;
        }

        public override Rank ChooseRankToAskFor()
        {
            LastRankAsked = rankToAsk;
            return rankToAsk;
        }

    }
}

