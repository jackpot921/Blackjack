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

        public Dealer(Card[] playingDeck, ref int count)
        {
            dealerHand = new Card[36];
            dealerCounter = 0;
            dealerHand[dealerCounter] = Game.DrawACard(playingDeck, ref count);
            dealerCounter++;
            dealerHand[dealerCounter] = Game.DrawACard(playingDeck, ref count);
            dealerCounter++;
        }

        public Card[] OneMoreCardToDealer(Card[] playingDeck, ref int count)
        {
            dealerHand[dealerCounter] = Game.DrawACard(playingDeck, ref count);
            dealerCounter++;
            return dealerHand;
        }
    }
}