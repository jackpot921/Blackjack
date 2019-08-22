using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        public static void CalculatingPoints(Card[] dealerHand, Card[] userHand, ref int userWinCounter, ref int dealerWinCounter, int dealerCounter, int userCounter)
        {
            Console.Clear();
            Console.WriteLine("Calculating results.");
            int dealerPoints = Deck.DeckValueCalculating(dealerHand, dealerCounter);
            int userPoints = Deck.DeckValueCalculating(userHand, userCounter);
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
                    Deck.PrintDeck(dealerHand, dealerCounter);
                    dealerWinCounter++;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Dealer wins with {dealerPoints} points.");
                    Console.WriteLine();
                    Console.WriteLine("Winner hand:");
                    Deck.PrintDeck(dealerHand, dealerCounter);
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
                    Deck.PrintDeck(userHand, userCounter);
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
                    Deck.PrintDeck(userHand, userCounter);
                    userWinCounter++;
                }
            }
        }
        public static bool CheckAces(Card[] dealerHand, Card[] userHand, ref int userWinCounter, ref int dealerWinCounter, int dealerCounter, int userCounter)
        {
            bool aces = false;
            if ((dealerHand[0].Face == Face.Ace && dealerHand[1].Face == Face.Ace))
            {
                Console.WriteLine("Dealer wins with two Aces.");
                Console.WriteLine();
                Console.WriteLine("Winner hand:");
                Deck.PrintDeck(dealerHand, dealerCounter);
                dealerWinCounter++;
                aces = true;
            }
            else if ((userHand[0].Face == Face.Ace && userHand[1].Face == Face.Ace))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You won with two Aces.");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Winner hand:");
                Deck.PrintDeck(userHand, userCounter);
                userWinCounter++;
                aces = true;
            }
            return aces;
        }
        public static Card[] DealHandToDealer(Card[] playingDeck, ref int count, ref int dealerCounter)
        {
            Card[] dealerHand;
            dealerHand = new Card[36];
            dealerHand[dealerCounter] = Deck.DrawACard(playingDeck, ref count);
            dealerCounter++;
            dealerHand[dealerCounter] = Deck.DrawACard(playingDeck, ref count);
            dealerCounter++;
            return dealerHand;
        }
        public static Card[] DealHandToUser(Card[] playingDeck, ref int count, ref int userCounter)
        {
            Card[] userHand;
            userHand = new Card[36];
            userHand[userCounter] = Deck.DrawACard(playingDeck, ref count);
            userCounter++;
            userHand[userCounter] = Deck.DrawACard(playingDeck, ref count);
            userCounter++;
            return userHand;
        }
        public static Card[] OneMoreCardToDealer(Card[] playingDeck, Card[] dealerDeck, ref int count, ref int dealerCounter)
        {
            dealerDeck[dealerCounter] = Deck.DrawACard(playingDeck, ref count);
            dealerCounter++;
            return dealerDeck;
        }
        public static Card[] OneMoreCardToUser(Card[] playingDeck, Card[] userDeck, ref int count, ref int userCounter)
        {
            userDeck[userCounter] = Deck.DrawACard(playingDeck, ref count);
            userCounter++;
            return userDeck;
        }
        public static void ChooseAnOptionIfDealerFirst(Card[] playingDeck, ref Card[] dealerHand, ref Card[] userHand, ref int count, ref int dealerCounter, ref int userCounter)
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
                            if (Deck.DeckValueCalculating(dealerHand, dealerCounter) <= 17)
                            {
                                Console.WriteLine("Dealer takes one more card.");
                                dealerHand = OneMoreCardToDealer(playingDeck, dealerHand, ref count, ref dealerCounter);
                            }
                            else
                            {
                                Console.WriteLine("Dealer decides to stay.");
                                if (Deck.DeckValueCalculating(userHand, userCounter) > 21)
                                {
                                    play = false;
                                    continue;
                                }
                            }
                            Console.WriteLine("You take one more card.");
                            userHand = OneMoreCardToUser(playingDeck, userHand, ref count, ref userCounter);
                            if (Deck.DeckValueCalculating(userHand, userCounter) <= 21)
                            {
                                Console.WriteLine($"Dealer have {dealerCounter} cards.");
                                Console.WriteLine();
                                Console.WriteLine("You have next cards:");
                                Deck.PrintDeck(userHand,userCounter);
                                Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand, userCounter)}");
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
                                if (Deck.DeckValueCalculating(dealerHand, dealerCounter) <= 17)
                                {
                                    Console.WriteLine("Dealer takes one more card.");
                                    dealerHand = OneMoreCardToDealer(playingDeck, dealerHand, ref count, ref dealerCounter);
                                    if (Deck.DeckValueCalculating(dealerHand, dealerCounter) > 21)
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
                            Console.WriteLine($"Dealer have {dealerCounter} cards.");
                            Console.WriteLine();
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(userHand, userCounter);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand, userCounter)}");
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
        public static void ChooseAnOptionIfUserFirst(Card[] playingDeck, ref Card[] dealerHand, ref Card[] userHand, ref int count, ref int userCounter, ref int dealerCounter)
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
                            userHand = OneMoreCardToUser(playingDeck, userHand, ref count, ref userCounter);
                            if (Deck.DeckValueCalculating(dealerHand, dealerCounter) <= 16)
                            {
                                Console.WriteLine("Dealer takes one more card.");
                                dealerHand = OneMoreCardToDealer(playingDeck, dealerHand, ref count, ref dealerCounter);
                                if (Deck.DeckValueCalculating(userHand, userCounter) > 21)
                                {
                                    play = false;
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Dealer decides to stay.");
                                if (Deck.DeckValueCalculating(userHand, userCounter) > 21)
                                {
                                    play = false;
                                    break;
                                }
                            }
                            Console.WriteLine($"Dealer have {dealerCounter} cards.");
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(userHand,userCounter);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand, userCounter)}");
                            Console.WriteLine();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case '2':
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("You decide to stay.");
                                if (Deck.DeckValueCalculating(dealerHand, dealerCounter) <= 17)
                                {
                                    Console.WriteLine("Dealer takes one more card.");
                                    dealerHand = OneMoreCardToDealer(playingDeck, dealerHand, ref count, ref dealerCounter);
                                }
                                else
                                {
                                    Console.WriteLine("Dealer decides to stay.");
                                    play = false;
                                    break;
                                }
                            }
                            while (play);
                            Console.WriteLine($"Dealer have {dealerCounter} cards.");
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(userHand,userCounter);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand, userCounter)}");
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
        public static void GameResults( int userWinCounter, int dealerWinCounter,out bool game )
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

        static void Main(string[] args)
        {
            int userWinCounter = 0;
            int dealerWinCounter = 0;
            bool game = true;
            while (game)
            {
                Card[] dealerHand;
                Card[] userHand;
                Card[] playingDeck = new Card[36];
                int dealerCounter = 0;
                int userCounter = 0;
                int count = 36;
                Deck.Initialize(playingDeck);
                Console.WriteLine("Choose who receive cards first: ");
                Console.WriteLine("1. Computer");
                Console.WriteLine("2. User");
                char decision = Convert.ToChar(Console.ReadLine());
                if (decision == '1')
                {
                    Console.Clear();
                    Console.WriteLine("Shuffling deck...");
                    Deck.Shuffle(playingDeck);
                    dealerHand = DealHandToDealer(playingDeck, ref count, ref dealerCounter);
                    Console.WriteLine($"Dealer takes {dealerCounter} cards.");
                    userHand = DealHandToUser(playingDeck, ref count, ref userCounter);
                    Console.WriteLine($"User takes {userCounter} cards");
                    Console.WriteLine();
                    Console.WriteLine("You have next cards:");
                    Deck.PrintDeck(userHand,userCounter);
                    Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand, userCounter)}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    if (!CheckAces(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter, dealerCounter, userCounter))
                    {
                        ChooseAnOptionIfDealerFirst(playingDeck, ref dealerHand, ref userHand, ref count, ref dealerCounter, ref userCounter);
                        CalculatingPoints(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter, dealerCounter, userCounter);
                    }
                    GameResults(userWinCounter, dealerWinCounter, out game);
                    continue;
                }else if (decision == '2')
                {
                    Console.Clear();
                    Console.WriteLine("Shuffling deck...");
                    Deck.Shuffle(playingDeck);
                    userHand = DealHandToUser(playingDeck, ref count, ref userCounter);
                    Console.WriteLine($"User takes {userCounter} cards");
                    dealerHand = DealHandToDealer(playingDeck, ref count, ref dealerCounter);
                    Console.WriteLine($"Dealer takes {dealerCounter} cards.");
                    Console.WriteLine();
                    Console.WriteLine("You have next cards:");
                    Deck.PrintDeck(userHand, userCounter);
                    Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand, userCounter)}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    if (!CheckAces(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter, dealerCounter, userCounter))
                    {
                        ChooseAnOptionIfUserFirst(playingDeck, ref dealerHand, ref userHand, ref count, ref userCounter, ref dealerCounter);
                        CalculatingPoints(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter, dealerCounter, userCounter);
                    }
                    GameResults( userWinCounter, dealerWinCounter, out game);
                    continue;
                }
                else
                {
                    Console.WriteLine("Incorrect Input!");
                    Console.WriteLine();
                    continue;
                }
                //if (char.TryParse(Console.ReadLine(), out char dec))
                //{
                //    char decision;
                //    decision = dec;
                //    switch (decision)
                //    {
                //        case '1':
                //            Console.Clear();
                //            Console.WriteLine("Shuffling deck...");
                //            Deck.Shuffle(playingDeck);
                //            dealerHand = DealHandToDealer(playingDeck, ref count, ref dealerCounter);
                //            Console.WriteLine($"Dealer takes {dealerCounter} cards.");
                //            userHand = DealHandToUser(playingDeck, ref count, ref userCounter);
                //            Console.WriteLine($"User takes {userCounter} cards");
                //            Console.WriteLine();
                //            Console.WriteLine("You have next cards:");
                //            Deck.PrintDeck(userHand);
                //            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand)}");
                //            Console.WriteLine("Press any key to continue...");
                //            Console.ReadKey();
                //            if (!CheckAces(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter))
                //            {
                //                ChooseAnOptionIfDealerFirst(playingDeck, ref dealerHand, ref userHand, ref count, ref dealerCounter, ref userCounter);
                //                CalculatingPoints(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter);
                //            }
                //            break;
                //        case '2':
                //            Console.Clear();
                //            Console.WriteLine("Shuffling deck...");
                //            Deck.Shuffle(playingDeck);
                //            userHand = DealHandToUser(playingDeck, ref count, ref userCounter);
                //            Console.WriteLine($"User takes {userCounter} cards");
                //            dealerHand = DealHandToDealer(playingDeck, ref count, ref dealerCounter);
                //            Console.WriteLine($"Dealer takes {dealerCounter} cards.");
                //            Console.WriteLine();
                //            Console.WriteLine("You have next cards:");
                //            Deck.PrintDeck(userHand);
                //            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand)}");
                //            Console.WriteLine("Press any key to continue...");
                //            Console.ReadKey();
                //            if (!CheckAces(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter))
                //            {
                //                ChooseAnOptionIfUserFirst(playingDeck, ref dealerHand, ref userHand, ref count, ref userCounter, ref dealerCounter);
                //                CalculatingPoints(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter);
                //            }
                //            break;
                //        default:
                //            Console.WriteLine("Incorrect Input!");
                //            Console.WriteLine();
                //            continue;
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("Incorrect Input!");
                //    Console.WriteLine();
                //    continue;
                //}
                //Console.WriteLine();
                //Console.WriteLine($"For current session:");
                //Console.WriteLine($"Your wins: {userWinCounter}");
                //Console.WriteLine($"Dealer wins: {dealerWinCounter}");
                //bool exit = false;
                //while (!exit)
                //{
                //    Console.WriteLine("If you want to exit type 'No', to play more type 'Yes'");
                //    string str = Console.ReadLine();
                //    if (str == "Yes" || str == "yes")
                //    {
                //        exit = true;
                //        continue;
                //    }
                //    else if (str == "No" || str == "no")
                //    {
                //        game = false;
                //        exit = true;
                //    }
                //    else
                //    {
                //        Console.WriteLine("Wrong Input!");
                //    }
                //}
            }
        }
    }
}

