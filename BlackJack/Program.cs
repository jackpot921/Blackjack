using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        public static Card[] userHand;
        public static Card[] dealerHand;

        public static void CalculatingPoints(Card[] dealerHand, Card[] userHand, ref int userWinCounter, ref int dealerWinCounter)
        {
            Console.Clear();
            Console.WriteLine("Calculating results.");
            int dealerPoints = Deck.DeckValueCalculating(dealerHand);
            int userPoints = Deck.DeckValueCalculating(userHand);
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
                    Deck.PrintDeck(dealerHand);
                    dealerWinCounter++;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Dealer wins with {dealerPoints} points.");
                    Console.WriteLine();
                    Console.WriteLine("Winner hand:");
                    Deck.PrintDeck(dealerHand);
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
                    Deck.PrintDeck(userHand);
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
                    Deck.PrintDeck(userHand);
                    userWinCounter++;
                }
            }
        }
        public static bool CheckAces(Card[] dealerHand, Card[] userHand, ref int userWinCounter, ref int dealerWinCounter)
        {
            bool aces = false;
            if ((dealerHand[0].Face == Face.Ace && dealerHand[1].Face == Face.Ace))
            {
                Console.WriteLine("Dealer wins with two Aces.");
                Console.WriteLine();
                Console.WriteLine("Winner hand:");
                Deck.PrintDeck(dealerHand);
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
                Deck.PrintDeck(userHand);
                userWinCounter++;
                aces = true;
            }
            return aces;
        }
        public static void DealHandToDealer(ref int count, ref int dealerCounter)
        {
            dealerHand = new Card[2];
            dealerHand[dealerCounter] = Deck.DrawACard(ref count);
            dealerCounter++;
            dealerHand[dealerCounter] = Deck.DrawACard(ref count);
            dealerCounter++;
        }
        public static void DealHandToUser(ref int count, ref int userCounter)
        {
            userHand = new Card[2];
            userHand[userCounter] = Deck.DrawACard(ref count);
            userCounter++;
            userHand[userCounter] = Deck.DrawACard(ref count);
            userCounter++;
        }
        public static Card[] OneMoreCardToDealer(ref Card[] dealerDeck, ref int count, ref int dealerCounter)
        {
            Array.Resize(ref dealerDeck, dealerCounter + 1);
            dealerDeck[dealerCounter] = Deck.DrawACard(ref count);
            dealerCounter++;
            return dealerDeck;
        }
        public static Card[] OneMoreCardToUser(ref Card[] userDeck, ref int count, ref int userCounter)
        {
            Array.Resize(ref userDeck, userCounter + 1);
            userDeck[userCounter] = Deck.DrawACard(ref count);
            userCounter++;
            return userDeck;
        }
        public static void ChooseAnOptionIfDealerFirst(ref int count, ref int dealerCounter, ref int userCounter)
        {
            bool play = true;
            do
            {

                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1. Take a card.");
                Console.WriteLine("2. Stay.");
                if (char.TryParse(Console.ReadLine(), out char dec))
                {
                    char decision;
                    decision = dec;
                    Console.Clear();
                    switch (decision)
                    {
                        case '1':
                            if (Deck.DeckValueCalculating(dealerHand) <= 17)
                            {
                                Console.WriteLine("Dealer takes one more card.");
                                OneMoreCardToDealer(ref dealerHand, ref count, ref dealerCounter);
                            }
                            else
                            {
                                Console.WriteLine("Dealer decides to stay.");
                                if (Deck.DeckValueCalculating(userHand) > 21)
                                {
                                    play = false;
                                    continue;
                                }
                            }
                            Console.WriteLine("You take one more card.");
                            OneMoreCardToUser(ref userHand, ref count, ref userCounter);
                            if (Deck.DeckValueCalculating(userHand) <= 21)
                            {
                                Console.WriteLine($"Dealer have {dealerCounter} cards.");
                                Console.WriteLine();
                                Console.WriteLine("You have next cards:");
                                Deck.PrintDeck(userHand);
                                Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand)}");
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
                                if (Deck.DeckValueCalculating(dealerHand) <= 17)
                                {
                                    Console.WriteLine("Dealer takes one more card.");
                                    OneMoreCardToDealer(ref dealerHand, ref count, ref dealerCounter);
                                    if (Deck.DeckValueCalculating(dealerHand) > 21)
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
                            Deck.PrintDeck(userHand);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand)}");
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
        public static void ChooseAnOptionIfUserFirst(ref int count, ref int userCounter, ref int dealerCounter)
        {
            bool play = true;
            do
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1. Take a card.");
                Console.WriteLine("2. Stay.");
                if (char.TryParse(Console.ReadLine(), out char dec))
                {
                    Console.Clear();
                    char decision = dec;
                    switch (decision)
                    {
                        case '1':
                            Console.WriteLine("You take one more card.");
                            OneMoreCardToUser(ref userHand, ref count, ref userCounter);
                            if (Deck.DeckValueCalculating(dealerHand) <= 16)
                            {
                                Console.WriteLine("Dealer takes one more card.");
                                OneMoreCardToDealer(ref dealerHand, ref count, ref dealerCounter);
                                if (Deck.DeckValueCalculating(userHand) > 21)
                                {
                                    play = false;
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Dealer decides to stay.");
                                if (Deck.DeckValueCalculating(userHand) > 21)
                                {
                                    play = false;
                                    break;
                                }
                            }
                            Console.WriteLine($"Dealer have {dealerCounter} cards.");
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(userHand);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand)}");
                            Console.WriteLine();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case '2':
                            do
                            {
                                Console.WriteLine("You decide to stay.");
                                if (Deck.DeckValueCalculating(dealerHand) <= 17)
                                {
                                    Console.WriteLine("Dealer takes one more card.");
                                    OneMoreCardToDealer(ref dealerHand, ref count, ref dealerCounter);
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
                            Deck.PrintDeck(userHand);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand)}");
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


        static void Main(string[] args)
        {
            int userWinCounter = 0;
            int dealerWinCounter = 0;
            bool game = true;
            while (game)
            {
                int dealerCounter = 0;
                int userCounter = 0;
                int count = 36;
                Deck.Initialize();
                Console.WriteLine("Choose who receive cards first: ");
                Console.WriteLine("1. Computer");
                Console.WriteLine("2. User");
                if (char.TryParse(Console.ReadLine(), out char dec))
                {
                    char decision;
                    decision = dec;
                    switch (decision)
                    {
                        case '1':
                            Console.Clear();
                            Console.WriteLine("Shuffling deck...");
                            Deck.Shuffle();
                            Deck.Shuffle();
                            DealHandToDealer(ref count, ref dealerCounter);
                            Console.WriteLine($"Dealer takes {dealerCounter} cards.");
                            DealHandToUser(ref count, ref userCounter);
                            Console.WriteLine($"User takes {userCounter} cards");
                            Console.WriteLine();
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(userHand);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand)}");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            if (!CheckAces(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter))
                            {
                                ChooseAnOptionIfDealerFirst(ref count, ref dealerCounter, ref userCounter);
                                CalculatingPoints(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter);
                            }
                            break;
                        case '2':
                            Console.Clear();
                            Console.WriteLine("Shuffling deck...");
                            Deck.Shuffle();
                            Deck.Shuffle();
                            DealHandToUser(ref count, ref userCounter);
                            Console.WriteLine($"User takes {userCounter} cards");
                            DealHandToDealer(ref count, ref dealerCounter);
                            Console.WriteLine($"Dealer takes {dealerCounter} cards.");
                            Console.WriteLine();
                            Console.WriteLine("You have next cards:");
                            Deck.PrintDeck(userHand);
                            Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(userHand)}");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            if (!CheckAces(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter))
                            {
                                ChooseAnOptionIfUserFirst(ref count, ref userCounter, ref dealerCounter);
                                CalculatingPoints(dealerHand, userHand, ref userWinCounter, ref dealerWinCounter);
                            }
                            break;
                        default:
                            Console.WriteLine("Incorrect Input!");
                            Console.WriteLine();
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect Input!");
                    Console.WriteLine();
                    continue;
                }
                Console.WriteLine();
                Console.WriteLine($"For current session:");
                Console.WriteLine($"Your wins: {userWinCounter}");
                Console.WriteLine($"Dealer wins: {dealerWinCounter}");
                bool yesOrNo = true;
                while (yesOrNo)
                {
                    Console.WriteLine("If you want to exit type 'No', to play more type 'Yes'");
                    string str = Console.ReadLine();
                    if (str == "Yes" || str == "yes")
                    {
                        yesOrNo = false;
                        continue;
                    }
                    else if (str == "No" || str == "no")
                    {
                        game = false;
                        yesOrNo = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input!");
                    }
                }
            }
        }
    }
}

