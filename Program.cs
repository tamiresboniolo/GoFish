using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    class Program
    {
        private static Deck myDeck;
        private static List<Player> players;
        private static int numTurns = 0;
        private static int numBooksPlayed = 0;
        //private static bool output = false;
        static void Main(string[] args)
        {

            Console.WriteLine("Author: Tamires Boniolo");
            Console.WriteLine();
            Console.WriteLine("GO FISH Simulation (Multiplayer)");
            Console.WriteLine("==================================================================================");
            Console.WriteLine();


            // GameResult gameResults = new GameResult();

            /*for (int gameNumber = 1; gameNumber <= 1000; gameNumber++)
            {
                numTurns = 0;
                numBooksPlayed = 0;*/

            //gameResults.AddRounds(round);
            myDeck = new Deck();
            myDeck.Shuffle();
            myDeck.Cut();

            //Creating 4 players
            players = new List<Player>
                {
                    new FirstCardRightPlayer("Tom"),
                    new LastCardLeftPlayer("Josh"),
                    new CheatingPlayer("Katy"),
                    new MemoryPlayer("Sean")
                };


            for (int i = 0; i < 5; i++)
            {
                foreach (Player player in players)
                    player.AddCardToHand(myDeck.DealCard());
            }

            foreach (Player player in players)
                Console.WriteLine(player.ToString());

            Console.WriteLine();

            int currentPlayerIndex = 0;
            Console.WriteLine("It is now " + players[currentPlayerIndex].Name + "'s turn.");

            while (true)
            {
                Player currentPlayer = players[currentPlayerIndex];

                Player playerToAsk = currentPlayer.ChoosePlayerToAsk(players);
                Rank rankToAskFor = currentPlayer.ChooseRankToAskFor();

                Console.WriteLine(currentPlayer.Name + " says: " + playerToAsk.Name + "! Give me all of your " + rankToAskFor + "s!");

                Card card = playerToAsk.GiveAnyCardOfRank(rankToAskFor);
                if (card == null)
                {
                    // playerToAsk doesn't have any cards of that rank.

                    Console.WriteLine(playerToAsk.Name + " says: GO FISH!");

                    if (myDeck.Size() > 0)
                    {
                        card = myDeck.DealCard();
                        Console.WriteLine(currentPlayer.Name + " draws a " + card + " from the deck.  The deck now has " + myDeck.Size() + " cards remaining.");
                        currentPlayer.AddCardToHand(card);
                        PlayBooks(currentPlayer);
                        if (IsGameOver()) break;
                        Draw5CardsIfHandIsEmpty(currentPlayer);
                    }
                    else
                    {
                        Console.WriteLine("Deck is empty. " + currentPlayer.Name + " cannot draw a card.");
                    }

                    Console.WriteLine(currentPlayer.Name + "'s turn is over. " + currentPlayer.Hand);
                    currentPlayerIndex = NextValidPlayer(currentPlayerIndex);
                    Console.WriteLine("\nIt is now " + players[currentPlayerIndex].Name + "'s turn.");
                    numTurns++;
                    DisplayScoreboard();
                }
                else
                {
                    // playerToAsk does have one (or more) cards of that rank. Take all of them.
                    do
                    {
                        Console.WriteLine(currentPlayer.Name + " gets the " + card + " from " + playerToAsk.Name);
                        currentPlayer.AddCardToHand(card);
                        card = playerToAsk.GiveAnyCardOfRank(rankToAskFor);
                    } while (card != null);

                    Draw5CardsIfHandIsEmpty(playerToAsk);

                    PlayBooks(currentPlayer);
                    if (IsGameOver()) break;
                    Draw5CardsIfHandIsEmpty(currentPlayer);

                    if (currentPlayer.Hand.Size() > 0)
                    {
                        Console.WriteLine("It is still " + currentPlayer.Name + "'s turn.");
                    }
                    else
                    {
                        Console.WriteLine(currentPlayer.Name + "'s hand is empty. " + currentPlayer.Name + " is finished.");
                        currentPlayerIndex = NextValidPlayer(currentPlayerIndex);
                        Console.WriteLine("\nIt is now " + players[currentPlayerIndex].Name + "'s turn.");
                        numTurns++;
                        DisplayScoreboard();
                    }

                }

            }

            Console.WriteLine("\n============== Game Over! =================\n");
            DisplayScoreboard();

            bool tieGame = false;
            Player winner = players[0];
            for (int i = 1; i < players.Count; i++)
            {
                if (players[i].Points > winner.Points)
                {
                    tieGame = false;
                    winner = players[i];
                }
                else if (players[i].Points == winner.Points)
                {
                    tieGame = true;
                }

            }

            Console.WriteLine("\nAfter " + numTurns + " turns,");
            if (tieGame)
                Console.WriteLine("It's a tie!");
            else
                Console.WriteLine("The winner is " + winner.Name + " with " + winner.Points + " points!");

            //}
            Console.ReadLine();
        }
        /* private static void OutputConsole(string message = "")
         {
             if (output == true)
             {
                 Console.WriteLine(message);
             }
         }*/

        private static void DisplayScoreboard()
        {
            Console.Write("SCORE: ");
            foreach (Player player in players)
                Console.Write(" | " + player.Name + ": " + player.Points);
            Console.WriteLine(" |  [Deck: " + myDeck.Size() + "]");
        }

        private static int NextValidPlayer(int currentPlayerIndex)
        {
            if (currentPlayerIndex < 0 || currentPlayerIndex >= players.Count)
            {
                throw new Exception("Need a valid player Index");
            }

            int nextPlayerIndex = currentPlayerIndex;
            do
            {
                nextPlayerIndex = (nextPlayerIndex + 1) % players.Count;
            } while (players[nextPlayerIndex].Hand.Size() == 0);

            return nextPlayerIndex;
        }

        private static void PlayBooks(Player player)
        {
            if (player == null)
            {
                throw new Exception("Need a valid player to play book");
            }

            Rank? rank = player.HasBookInHand();
            while (rank != null)
            {
                Console.WriteLine(">>> " + player.Name.ToUpper() + " HAS A BOOK! PLAYING A BOOK OF " + rank.ToString().ToUpper() + "S!");
                player.PlayBook((Rank)rank);
                numBooksPlayed++;
                rank = player.HasBookInHand();
            }
        }

        private static bool IsGameOver()
        {
            return (numBooksPlayed == Enum.GetValues(typeof(Rank)).Length);
        }

        private static void Draw5CardsIfHandIsEmpty(Player player)
        {
            if (player == null)
            {
                throw new Exception("Need a valid player to draw card");
            }

            if (player.Hand.Size() > 0) return;
            if (myDeck.Size() == 0) return;

            Console.WriteLine(">>>> " + player.Name + "'s hand is empty.  Drawing up to 5 cards from the deck. <<<<");
            for (int i = 0; i < 5; i++)
            {
                Card card = myDeck.DealCard();
                if (card == null)
                    break;
                player.Hand.AddCard(card);
                PlayBooks(player);
            }
        }
    }
}


