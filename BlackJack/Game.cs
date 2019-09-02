﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public struct Game
    {
        public Card[] playingDeck;
        public int count;
        public int dealerWinCounter;
        public int userWinCounter;

        public void Initialize(Card[] cards)
        {
            playingDeck = cards;
            count = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    playingDeck[count] = (new Card() { Suit = (Suit)i, Face = (Face)j });
                    if (j <= 4)
                    {
                        playingDeck[count].Value = j + 6;
                        count++;
                    }
                    else if (j >= 5 && j <= 7)
                    {
                        playingDeck[count].Value = j - 3;
                        count++;
                    }
                    else
                    {
                        playingDeck[count].Value = 11;
                        count++;
                    }
                }
            }
        }

        public static Card DrawACard(Card[] playingDeck, ref int count)
        {
            count--;
            return playingDeck[count];
        }

        public void CalculatingPoints(Dealer dealer, User user)
        {
            Console.Clear();
            Console.WriteLine("Calculating results.");
            int dealerPoints = Deck.DeckValueCalculating(dealer.dealerHand);
            int userPoints = Deck.DeckValueCalculating(user.userHand);
            Console.WriteLine($"Dealer has {dealerPoints} points.");
            Console.WriteLine($"You have {userPoints} points.");
            if (dealerPoints == userPoints)
            {
                Console.WriteLine("It's a draw, no one wins!");
            }
            if (dealerPoints > userPoints && dealerPoints <= 21 || dealerPoints < userPoints && userPoints > 21)
            {
                if (dealerPoints == 21)
                {
                    Console.WriteLine();
                    Console.WriteLine("Dealer wins with BlackJack!");
                    Console.WriteLine();
                    Console.WriteLine("Winner hand:");
                    Deck.PrintDeck(dealer.dealerHand);
                    dealerWinCounter++;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Dealer wins with {dealerPoints} points.");
                    Console.WriteLine();
                    Console.WriteLine("Winner hand:");
                    Deck.PrintDeck(dealer.dealerHand);
                    dealerWinCounter++;
                }

            }
            else if (userPoints > dealerPoints && userPoints <= 21 || userPoints < dealerPoints && dealerPoints > 21)
            {
                if (userPoints == 21)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You won with BlackJack!");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("Winner hand:");
                    Deck.PrintDeck(user.userHand);
                    userWinCounter++;
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You won with {userPoints} points.");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("Winner hand:");
                    Deck.PrintDeck(user.userHand);
                    userWinCounter++;
                }
            }
        }

        public bool CheckAces(Dealer dealer, User user)
        {

            bool aces = false;
            if ((dealer.dealerHand[0].Face == Face.Ace && dealer.dealerHand[1].Face == Face.Ace))
            {
                Console.WriteLine("Dealer wins with two Aces.");
                Console.WriteLine();
                Console.WriteLine("Winner hand:");
                Deck.PrintDeck(dealer.dealerHand);
                dealerWinCounter++;
                aces = true;
            }
            else if ((user.userHand[0].Face == Face.Ace && user.userHand[1].Face == Face.Ace))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You won with two Aces.");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Winner hand:");
                Deck.PrintDeck(user.userHand);
                userWinCounter++;
                aces = true;
            }
            return aces;
        }

        public void ChooseAnOptionIfDealerFirst(Dealer dealer, User user)
        {

            bool play = true;
            do
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1. Take a card.");
                Console.WriteLine("2. Stay.");
                if (char.TryParse(Console.ReadLine(), out char dec))
                {
                    switch (dec)
                    {
                        case '1':
                            Console.Clear();
                            if (Deck.DeckValueCalculating(dealer.dealerHand) <= 17)
                            {
                                Console.WriteLine("Dealer takes one more card.");
                                dealer.dealerHand = dealer.OneMoreCardToDealer(playingDeck, ref count);
                            }
                            else
                            {
                                Console.WriteLine("Dealer decides to stay.");
                                if (Deck.DeckValueCalculating(user.userHand) > 21)
                                {
                                    play = false;
                                    continue;
                                }
                            }
                            Console.WriteLine("You take one more card.");
                            user.userHand = user.OneMoreCardToUser(playingDeck, ref count);
                            if (Deck.DeckValueCalculating(user.userHand) <= 21)
                            {
                                Console.WriteLine($"Dealer have {dealer.dealerCounter} cards.");
                                Console.WriteLine();
                                Console.WriteLine("You have next cards:");
                                Deck.PrintDeck(user.userHand);
                                Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(user.userHand)}");
                                Console.WriteLine();
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                play = false;
                            }
                            break;
                        case '2':
                            do
                            {
                                Console.Clear();
                                if (Deck.DeckValueCalculating(dealer.dealerHand) <= 17)
                                {
                                    Console.WriteLine("Dealer takes one more card.");
                                    dealer.dealerHand = dealer.OneMoreCardToDealer(playingDeck, ref count);
                                    if (Deck.DeckValueCalculating(dealer.dealerHand) > 21)
                                    {
                                        play = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Dealer decides to stay.");
                                    play = false;
                                }
                            } while (play);
                            Console.WriteLine("You decide to stay.");
                            Console.WriteLine($"Dealer have {dealer.dealerCounter} cards.");
                            Console.WriteLine();
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(user.userHand);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(user.userHand)}");
                            Console.WriteLine();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Wrong Input");
                            break;
                    }
                }
            }
            while (play);
        }

        public void ChooseAnOptionIfUserFirst(Dealer dealer, User user)
        {
            bool play = true;
            do
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1. Take a card.");
                Console.WriteLine("2. Stay.");
                if (char.TryParse(Console.ReadLine(), out char dec))
                {
                    switch (dec)
                    {
                        case '1':
                            Console.Clear();
                            Console.WriteLine("You take one more card.");
                            user.userHand = user.OneMoreCardToUser(playingDeck, ref count);
                            if (Deck.DeckValueCalculating(dealer.dealerHand) <= 16)
                            {
                                Console.WriteLine("Dealer takes one more card.");
                                dealer.dealerHand = dealer.OneMoreCardToDealer(playingDeck, ref count);
                                if (Deck.DeckValueCalculating(user.userHand) > 21)
                                {
                                    play = false;
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Dealer decides to stay.");
                                if (Deck.DeckValueCalculating(user.userHand) > 21)
                                {
                                    play = false;
                                    break;
                                }
                            }
                            Console.WriteLine($"Dealer have {dealer.dealerCounter} cards.");
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(user.userHand);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(user.userHand)}");
                            Console.WriteLine();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case '2':
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("You decide to stay.");
                                if (Deck.DeckValueCalculating(dealer.dealerHand) <= 17)
                                {
                                    Console.WriteLine("Dealer takes one more card.");
                                    dealer.dealerHand = dealer.OneMoreCardToDealer(playingDeck, ref count);
                                }
                                else
                                {
                                    Console.WriteLine("Dealer decides to stay.");
                                    play = false;
                                    break;
                                }
                            }
                            while (play);
                            Console.WriteLine($"Dealer have {dealer.dealerCounter} cards.");
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(user.userHand);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(user.userHand)}");
                            Console.WriteLine();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Wrong Input");
                            break;
                    }
                }
            }
            while (play);
        }

        public void GameResults(out bool game)
        {
            Console.WriteLine();
            Console.WriteLine($"For current session:");
            Console.WriteLine($"Your wins: {userWinCounter}");
            Console.WriteLine($"Dealer wins: {dealerWinCounter}");
            bool exit = false;
            game = true;
            while (!exit)
            {
                Console.WriteLine("If you want to exit type 'No', to play more type 'Yes'");
                string str = Console.ReadLine();
                if (str == "Yes" || str == "yes")
                {
                    exit = true;
                    continue;
                }
                else if (str == "No" || str == "no")
                {
                    game = false;
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Wrong Input!");
                }
            }
        }
    }
}