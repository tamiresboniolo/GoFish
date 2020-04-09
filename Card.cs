
//Author: Tamires Boniolo

using System;

namespace GoFish
{
    public enum Suit { Clubs, Diamonds, Hearts, Spades };
    public enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };


    public class Card : IComparable<Card>
    {
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }

        public Card(Suit suit, Rank rank)
        {
            this.Suit = suit;
            this.Rank = rank;
        }

        public int CompareTo(Card other)
        {
            if (other == null)
            {
                throw new Exception("Need a valid card to compare");
            }

            if (Rank != other.Rank)
                return Rank.CompareTo(other.Rank);

            // if rank are the same, so compare suits as a fallback
            return Suit.CompareTo(other.Suit);
        }

        public override string ToString()
        {
            return ("[" + Rank + " of " + Suit + "]");
        }


    }
}