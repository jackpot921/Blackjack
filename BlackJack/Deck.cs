using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public struct Deck
    {
        public static int DeckValueCalculating(Card[] cards, int counter)
        {
            int deckValue = 0;
            for (int i = 0; i < counter; i++)
            {
                int cardValue = 0;
                switch (cards[i].Face)
                {
                    case Face.Six:
                        cardValue = 6;
                        break;
                    case Face.Seven:
                        cardValue = 7;
                        break;
                    case Face.Eight:
                        cardValue = 8;
                        break;
                    case Face.Nine:
                        cardValue = 9;
                        break;
                    case Face.Ten:
                        cardValue = 10;
                        break;
                    case Face.Jack:
                        cardValue = 2;
                        break;
                    case Face.Queen:
                        cardValue = 3;
                        break;
                    case Face.King:
                        cardValue = 4;
                        break;
                    case Face.Ace:
                        cardValue = 11;
                        break;
                }
                deckValue += cardValue;
            }
            return deckValue;
        }

        public static Card DrawACard(Card[] cards, ref int count)
        {
            count--;
            return cards[count];
        }

        public static Card[] Initialize(Card[] cards)
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cards[index] = (new Card() { Suit = (Suit)i, Face = (Face)j });
                    index++;
                }
            }
            return cards;
        }

        public static void PrintDeck(Card[] cards, int counter)
        {
            for (int i = 0; i < counter; i++)
            {
                int cardValue = 0;
                switch (cards[i].Face)
                {
                    case Face.Six:
                        cardValue = 6;
                        break;
                    case Face.Seven:
                        cardValue = 7;
                        break;
                    case Face.Eight:
                        cardValue = 8;
                        break;
                    case Face.Nine:
                        cardValue = 9;
                        break;
                    case Face.Ten:
                        cardValue = 10;
                        break;
                    case Face.Jack:
                        cardValue = 2;
                        break;
                    case Face.Queen:
                        cardValue = 3;
                        break;
                    case Face.King:
                        cardValue = 4;
                        break;
                    case Face.Ace:
                        cardValue = 11;
                        break;
                }
                Console.WriteLine($"Card{i + 1}: {cards[i].Face} of {cards[i].Suit} ({cardValue} points.)");
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