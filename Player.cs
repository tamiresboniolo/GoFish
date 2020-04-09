//Author: Tamires Boniolo

using System;
using System.Collections.Generic;

namespace GoFish
{
    public abstract class Player
    {
        public string Name { get; private set; }
        public Hand Hand { get; private set; }
        public int Points { get; private set; }
        public Rank LastRankAsked { get; protected set; }

        public Player(string name)
        {
            this.Name = name;
            this.Hand = new Hand();
        }

        public abstract Player ChoosePlayerToAsk(List<Player> players);

        public abstract Rank ChooseRankToAskFor();


        public void AddCardToHand(Card card)
        {
            if (card == null)
            {
                throw new Exception("Need a valid card to add");
            }

            Hand.AddCard(card);
        }

        public Card GiveAnyCardOfRank(Rank rank)
        {
            foreach (Card c in Hand.GetCards())
            {
                if (c.Rank == rank)
                {
                    Hand.RemoveCard(c);
                    return c;
                }
            }
            return null;
        }


        public bool HasRankInHand(Rank rank)
        {
            foreach (Card c in Hand.GetCards())
            {
                if (c.Rank == rank)
                    return true;
            }
            return false;
        }


        // Returns the rank of the first book found.  Returns null if no books are found.
        public Rank? HasBookInHand()
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                int numSuits = 0;
                foreach (Card c in Hand.GetCards())
                {
                    if (c.Rank == rank)
                        numSuits++;
                }

                if (numSuits == Enum.GetValues(typeof(Suit)).Length)
                    return rank;
            }
            return null;
        }


        public void PlayBook(Rank rank)
        {
            List<Card> cardsToRemove = new List<Card>();

            foreach (Card c in Hand.GetCards())
            {
                if (c.Rank == rank)
                {
                    cardsToRemove.Add(c);
                }
            }

            foreach (Card c in cardsToRemove)
            {
                Hand.RemoveCard(c);
            }

            Points++;
        }

        public override string ToString()
        {
            string s = Name + "'s Hand: ";
            s += Hand.ToString();

            return s;
        }
    }
}