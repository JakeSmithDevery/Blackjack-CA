using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_CA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //shows the player the games name 
            Console.WriteLine("Welcome to Blackjack!");
            //allows the player to play again
            while (true)
            {
                PlayBlackjack();
                Console.Write("Do you want to play again? (y/n): ");
                if (Console.ReadLine().ToLower() != "y")
                    break;
            }
        }

        static void PlayBlackjack()
        {
            //New deck and shuffle them
            Deck deck = new Deck();
            deck.Shuffle();

            // Create hands for player and dealer
            Hand playerHand = new Hand();
            Hand dealerHand = new Hand();

            // Deal first cards for player and dealer
            playerHand.AddCard(deck.DrawCard());
            dealerHand.AddCard(deck.DrawCard());
            playerHand.AddCard(deck.DrawCard());
            dealerHand.AddCard(deck.DrawCard());

            // Show both hands
            Console.WriteLine("Your hand: ");
            playerHand.Display();
            Console.WriteLine("Dealer's hand: ");
            dealerHand.DisplayDealer();

            // Player turn 
            while (true)
            {

                //ask to hit or stand
                Console.Write("Do you want to Hit or Stand? (h/s): ");
                char choice = Console.ReadLine().ToLower()[0];

                //if the player hits draw a new card and display it
                if (choice == 'h')
                {
                    playerHand.AddCard(deck.DrawCard());
                    Console.WriteLine("Your hand: ");
                    playerHand.Display();

                    //tell the player if theve gone bust
                    if (playerHand.GetTotal() > 21)
                    {
                        Console.WriteLine("Bust! You lose.");
                        return;
                    }
                }

                //if player stands exit the code for the player
                else if (choice == 's')
                {
                    break;
                }
            }

            // Dealer turn 
            Console.WriteLine("Dealer's turn:");
            dealerHand.Display();
            //if dealer under 17 he hits
            while (dealerHand.GetTotal() < 17)
            {
                dealerHand.AddCard(deck.DrawCard());
                dealerHand.Display();

                //if dealer busts he loses
                if (dealerHand.GetTotal() > 21)
                {
                    Console.WriteLine("Dealer busts! You win.");
                    return;
                }
            }

            // Decide winner
            int playerTotal = playerHand.GetTotal();
            int dealerTotal = dealerHand.GetTotal();

            // display winner

            if (playerTotal > dealerTotal)
            {
                Console.WriteLine("You win!");
            }
            else if (dealerTotal > playerTotal)
            {
                Console.WriteLine("Dealer wins.");
            }
            else
            {
                Console.WriteLine("It's a tie.");
            }
        }
    }

    class Deck
    {
        //create list and random
        private List<Card> cards;
        private Random random = new Random();


        public Deck()
        {
            //create the cards
            cards = new List<Card>();
            foreach (string suit in new[] { "Hearts", "Diamonds", "Clubs", "Spades" })
            {
                for (int rank = 2; rank <= 10; rank++)
                {
                    cards.Add(new Card(rank.ToString(), suit));
                }
                cards.Add(new Card("Jack", suit));
                cards.Add(new Card("Queen", suit));
                cards.Add(new Card("King", suit));
                cards.Add(new Card("Ace", suit));
            }
        }

        public void Shuffle()
        {
            //place cards in random order "shuffleing"thgem
            for (int i = 0; i < cards.Count; i++)
            {
                int j = random.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        //draw a random card for the created dech
        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

    class Card
    {
        public string Rank { get; }
        public string Suit { get; }

        public Card(string rank, string suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public int GetValue()
        {
            //find the value of the card drawn
            if (Rank == "Ace")
 
                return 11;
            if (Rank == "King" || Rank == "Queen" || Rank == "Jack")
                return 10;
            return int.Parse(Rank);
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }

    class Hand
    {
        private List<Card> cards = new List<Card>();

        public void AddCard(Card card)
        {
            //add a card to the hand
            cards.Add(card);
        }

        public int GetTotal()
        {
            int total = 0;
            int aces = 0;
            //add the value of the card and check for aces 
            foreach (var card in cards)
            {
                total += card.GetValue();
                if (card.Rank == "Ace")
                {
                    aces++;
                }
            }
            // if an ace is in the players hand and they go bust change the ace from a 11 to a 1
            while (total > 21 && aces > 0)
            {
                total -= 10;
                aces--;
            }

            return total;
        }

        public void Display()
        {
            //display the card and total
            foreach (var card in cards)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine($"Total: {GetTotal()}");
        }

        public void DisplayDealer()
        {
            //display cards and total for dealer
            Console.WriteLine(cards[0]);
            Console.WriteLine("Total: ?");
        }
    }
}

