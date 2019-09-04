using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public struct Dealer
    {
        public Card[] dealerHand;
        public int dealerCounter;

        public Dealer(Card[] playingDeck, int count)
        {
            dealerHand = new Card[36];
            dealerCounter = 0;
            dealerHand = OneMoreCardToDealer(playingDeck, count);
            dealerHand = OneMoreCardToDealer(playingDeck, --count);
            Game.cardIndex = count;
        }

        public Card[] OneMoreCardToDealer(Card[] playingDeck, int count)
        {
            dealerHand[dealerCounter] = playingDeck[count];
            dealerCounter++;
            return dealerHand;
        }
    }
}