//Author: Tamires Boniolo

using System;
using System.Collections.Generic;
using System.Linq;


namespace GoFish
{
    public class Deck
    {
        private Stack<Card> Cards;
        private static Random randomDeck = new Random();       // static helps prevent duplicate rng's

        public Deck()
        {
            List<Suit> suits = new List<Suit>((Suit[])Enum.GetValues(typeof(Suit)));
            List<Rank> ranks = new List<Rank>((Rank[])Enum.GetValues(typeof(Rank)));

            int size = suits.Count * ranks.Count;
            Cards = new Stack<Card>();

            foreach (Suit suit in suits)
            {
                foreach (Rank rank in ranks)
                {
                    Card card = new Card(suit, rank);
                    Cards.Push(card);
                }

            }
        }

        public int Size()
        {
            return Cards.Count;
        }


        public void Shuffle()
        {
            Stack<Card> NewCards = new Stack<Card>();
            List<Card> toShuffle = Cards.ToList();
            while (toShuffle.Count > 0)
            {
                int CardToMove = randomDeck.Next(toShuffle.Count);
                NewCards.Push(toShuffle[CardToMove]);
                toShuffle.RemoveAt(CardToMove);
            }
            Cards = NewCards;
        }

        public void Cut()
        {
            if (Size() == 0) return; // Cannot cut an empty deck

            int cutPoint = randomDeck.Next(1, Size()); // Cannot cut at zero

            Stack<Card> newDeck = new Stack<Card>();

            int x;
            // Push from after the cutpoint to the bottom of the Stack
            for (x = 0; x < cutPoint; x++)
            {
                newDeck.Push(Cards.ElementAt(x));
            }

            // Push the rest on the top
            for (x = cutPoint; x < Size(); x++)
            {
                newDeck.Push(Cards.ElementAt(x));
            }

            Cards = newDeck;
        }

        public Card DealCard()
        {
            if (Size() == 0) return null;

            Card card = Cards.Pop();

            return card;
        }

        public void ReturnCardToDeck(Card c)
        {
            if (c == null) return;
            Cards.Push(c);      // Adds card to the bottom of the deck           
        }

        public override string ToString()
        {
            string s = "[";
            string comma = "";
            foreach (Card c in Cards)
            {
                s += comma + c.ToString();
                comma = ", ";
            }
            s += "]";
            s += "\n " + Size() + " cards in deck.\n";

            return s;
        }

    }
}
