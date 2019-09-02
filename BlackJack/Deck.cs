using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public struct Deck
    {
        public static int DeckValueCalculating(Card[] cards)
        {
            int deckValue = 0;
            foreach (var card in cards)
            {
                deckValue += card.Value;
            }
            return deckValue;
        }

        public static void PrintDeck(Card[] cards)
        {
            foreach (var item in cards)
            {
                if (item.Value > 0)
                {
                    Console.WriteLine($"{item.Face} of {item.Suit} ({item.Value} points.)");
                }
            }
        }

        public static void Shuffle(Card[] cards)
        {
            Random rnd = new Random();
            int length = cards.Length;
            while (length > 1)
            {
                length--;
                int index = rnd.Next(length + 1);
                Card card = cards[index];
                cards[index] = cards[length];
                cards[length] = card;
            }
        }
    }
}