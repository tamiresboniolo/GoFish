//Author: Tamires Boniolo

using System.Collections.Generic;

namespace GoFish
{
    public class Hand
    {
        private List<Card> Cards;

        public Hand()
        {
            Cards = new List<Card>();
        }

        public int Size()
        {
            return Cards.Count;
        }

        public List<Card> GetCards()
        {
            return Cards;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public Card RemoveCard(Card card)
        {
            if (Cards.Contains(card))
            {
                Cards.Remove(card);
                return card;
            }
            return null;
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

            return s;
        }

    }
}

