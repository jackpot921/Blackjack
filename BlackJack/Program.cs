using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Card[] cards = new Card[36];
            Game game = new Game();
            bool play = true;

            while (play)
            {
                game.Initialize(cards);
                Console.WriteLine("Choose who receive cards first: ");
                Console.WriteLine("1. Computer");
                Console.WriteLine("2. User");
                string decision = Console.ReadLine();
                if (decision == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Shuffling deck...");
                    Deck.Shuffle(game.playingDeck);
                    Dealer dealer = new Dealer(game.playingDeck, --Game.cardIndex);
                    Console.WriteLine($"Dealer takes {dealer.dealerCounter} cards.");
                    User user = new User(game.playingDeck, --Game.cardIndex);
                    Console.WriteLine($"User takes {user.userCounter} cards");
                    Console.WriteLine();
                    Console.WriteLine("You have next cards:");
                    Deck.PrintDeck(user.userHand);
                    Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(user.userHand)}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    if (!game.CheckAces(dealer, user))
                    {
                        game.ChooseAnOptionIfDealerFirst(dealer, user);
                        game.CalculatingPoints(dealer, user);
                    }
                    play = game.GameResults();
                }
                else if (decision == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Shuffling deck...");
                    Deck.Shuffle(game.playingDeck);
                    User user = new User(game.playingDeck, --Game.cardIndex);
                    Console.WriteLine($"User takes {user.userCounter} cards");
                    Dealer dealer = new Dealer(game.playingDeck, --Game.cardIndex);
                    Console.WriteLine($"Dealer takes {dealer.dealerCounter} cards.");
                    Console.WriteLine();
                    Console.WriteLine("You have next cards:");
                    Deck.PrintDeck(user.userHand);
                    Console.WriteLine($"Value of your hand: {Deck.DeckValueCalculating(user.userHand)}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    if (!game.CheckAces(dealer, user))
                    {
                        game.ChooseAnOptionIfUserFirst(dealer, user);
                        game.CalculatingPoints(dealer, user);
                    }
                    play = game.GameResults();
                }
                else
                {
                    Console.WriteLine("Incorrect Input!");
                    Console.WriteLine();
                }
            }
        }
    }
}