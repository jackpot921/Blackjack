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

        public static Card DrawACard(Card[] cards, int count, out int counter)
        {
            counter = count;
            counter--;
            return cards[counter];
        }

        public static Card[] Initialize(Card[] cards)
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cards[index] = (new Card() { Suit = (Suit)i, Face = (Face)j });
                    if(j <= 4)
                    {
                        cards[index].Value = j + 6;
                        index++;
                    }else if(j >= 5 && j <= 7)
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
            foreach (var item in cards)
            {
                if(item.Value > 0)
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