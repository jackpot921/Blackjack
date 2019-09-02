using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public struct User
    {
        public Card[] userHand;
        public int userCounter;

        public User(Card[] playingDeck, ref int count)
        {
            userHand = new Card[36];
            userCounter = 0;
            userHand[userCounter] = Game.DrawACard(playingDeck, ref count);
            userCounter++;
            userHand[userCounter] = Game.DrawACard(playingDeck, ref count);
            userCounter++;
        }

        public Card[] OneMoreCardToUser(Card[] playingDeck, ref int count)
        {
            userHand[userCounter] = Game.DrawACard(playingDeck, ref count);
            userCounter++;
            return userHand;
        }
    }
}
