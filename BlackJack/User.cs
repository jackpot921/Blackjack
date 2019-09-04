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

        public User(Card[] playingDeck, int count)
        {
            userHand = new Card[36];
            userCounter = 0;
            userHand = OneMoreCardToUser(playingDeck, count);
            userHand = OneMoreCardToUser(playingDeck, --count);
            Game.cardIndex = count;
        }

        public Card[] OneMoreCardToUser(Card[] playingDeck, int count)
        {
            userHand[userCounter] = playingDeck[count];
            userCounter++;
            return userHand;
        }
    }
}