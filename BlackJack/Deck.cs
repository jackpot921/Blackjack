using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public struct Deck
    {
        public static Card[] cards;

        public static int DeckValueCalculating(Card[] cards)
        {
            int value = 0;
            foreach (var card in cards)
            {
                value += card.Value;
            }
            return value;
        }

        public static Card DrawACard(ref int count)
        {
            count--;
            return cards[count];
        }

        public static Card[] Initialize()
        {
            cards = new Card[36];
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cards[index] = (new Card() { Suit = (Suit)i, Face = (Face)j });

                    if (j <= 4)
                    {
                        cards[index].Value = j + 6;
                        index++;
                    }
                    else if ((j >= 5 && j <= 7))
                    {
                        cards[index].Value = j - 3;
                        index++;
                    }
                    else
                    {
                        cards[index].Value = 11;
                        index++;
                    }
                }
            }
            return cards;
        }

        public static void PrintDeck(Card[] cards)
        {
            int num = 1;
            foreach (var card in cards)
            {
                Console.WriteLine($"Card{num}: {card.Face} of {card.Suit} ({card.Value} points.)");
                num++;
            }
        }

        public static void Shuffle()
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