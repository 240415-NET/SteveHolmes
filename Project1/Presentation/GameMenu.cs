using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NoughtsAndCrosses.Controllers;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Presentation;

public class GameMenu
{
    public static void Start(User user)
    {

        int userChoice = 0;
        bool validInput = true;
        bool keepAlive = true;

        Console.Clear();
        do
        {
            if (validInput)
            {
                Console.Clear();
                Console.WriteLine($"\nYou are playing NOUGHTS AND CROSSES\n");
                Console.WriteLine($"Hello {user.userName}. Please select one of the following options:\n");
                Console.WriteLine("1. Start a new game");
                Console.WriteLine("2. Continue playing an earlier game");
                Console.WriteLine("3. Review game history");
                Console.WriteLine("4. Clear game history");
                Console.WriteLine("5. View the scoreboard");
                Console.WriteLine("6. Exit\n");
            }

            try
            {
                if (validInput) Console.Write(": ");
                userChoice = GameController.NumberFromUser();
                validInput = true;

                switch (userChoice)
                {
                    case 1: // start a new game
                        StartGame(user);
                        break;
                    case 2: // continue playing a previous game
                        ContinuePlayingAnEarlierGame(user);
                        break;
                    case 3: // review game history   
                        ReviewGameHistory(user);
                        break;
                    case 4: // clear game history                      
                        ClearGameHistory(user);
                        break;
                    case 5: // view the scoreboard                     
                        ViewScoreboard();
                        break;
                    case 6: //exit 
                        DisplayExitingMessage(user);                    
                        keepAlive = false;
                        break;

                    default: // If the user enters an integer that is not in 1..6
                        Console.Write("Please enter one of the above options: ");
                        validInput = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                validInput = false;
                Console.Write("Please enter a number for option that you want to choose: ");
            }
        } while (!validInput || keepAlive);
    }

    public static void HitAnyKeyToContinue()
    {
        Console.Write("\nHit any key to continue...");
        Console.ReadKey();
    }

    public static void StartGame(User user)
    // entry point for a new game
    {
        Game newGame = new Game(user);
        StartGame(user, newGame);
        GameController.SaveGame(newGame);
    }

    public static void StartGame(User user, int gameNumber)
    // entry point to continue playing an existing game
    {
        Game existingGame = GameController.NumberedGameAt(user, gameNumber);
        StartGame(user, existingGame);
        GameController.UpdateGame(existingGame);
    }

    public static void StartGame(User user, Game game)
    {
        PlayGame.Play(user, game);

        HitAnyKeyToContinue();
        Console.Clear();
    }

    public static void ContinuePlayingAnEarlierGame(User user)
    {
        Console.Clear();
        Console.WriteLine($"\nNOUGHTS AND CROSSES - Currently Active Games for {user.userName}\n");
        Console.WriteLine("No   Start Date/Time                ");
        Console.WriteLine("---- ------------------------------ ");

        List<string> gamesListing = GameController.NumberedGamesInProgress(user);

        if (gamesListing.Count == 0) {
            Console.WriteLine("\nYou don't currently have any active games.\n\n");
            HitAnyKeyToContinue();
            Console.Clear();
            return;
        }
            
        foreach (string each in gamesListing)
            Console.WriteLine(each);   
    
        Console.Write("\n\nEnter the number of a game to continue playing: ");
        int whichOne = GameNumberFromUser(gamesListing.Count);
        StartGame(user, whichOne - 1);
    }

    public static int GameNumberFromUser(int numberOfGames)
    {
        int userEntry = GameController.NumberFromUser();
        while (userEntry < 1 || userEntry > numberOfGames)
        {
            Console.Write("Invalid entry. Please select a game number: ");
            userEntry = GameController.NumberFromUser();
        }
        return userEntry;
    }

    public static void ReviewGameHistory(User user)
    {
        Console.Clear();
        Console.WriteLine($"\nNOUGHTS AND CROSSES - Game History for {user.userName}\n");
        Console.WriteLine("Start Date/Time                    End Date/Time                      Win/Loss/Draw");
        Console.WriteLine("------------------------------     ------------------------------     -------------");

        List<string> gamesListing = GameController.AllGamesAsText(user);
        if (gamesListing.Count == 0)
            Console.WriteLine("\nYou currently don't have any game history to display\n\n");
        else foreach (string each in gamesListing)
            Console.WriteLine(each);        

        Console.WriteLine("\n"); 
        HitAnyKeyToContinue();
        Console.Clear();
    }    

    public static void ClearGameHistory(User user)
    {
        Console.Clear();
        Console.WriteLine($"\nNOUGHTS AND CROSSES - Clear Game History for {user.userName}\n");
        Console.WriteLine("\tWARNING\n");
        Console.WriteLine("\tYOU ARE ABOUT TO DELETE YOUR CURRENT GAME HISTORY.");
        Console.WriteLine("\tTHIS WILL REMOVE ALL RECORDS OF YOUR GAMES,");
        Console.WriteLine("\tAND WILL REMOVE YOU FROM THE SCOREBOARD.\n");
        Console.WriteLine("Are you sure you want to continue?");
        Console.WriteLine("   Enter Y to clear your game history");
        Console.WriteLine("   Enter N to retain your game history\n");
        Console.Write(": ");
        
        string entry = Console.ReadLine().ToUpper();

        if (entry == "Y")
        {
            GameController.ClearGameHistory(user);
            Console.WriteLine("\nYour game history has been cleared.");
        }
        else
        {
            Console.WriteLine("\nYou have chosen to not clear your game history.");
        }
        HitAnyKeyToContinue();
        Console.Clear();
    }
  
    public static void ViewScoreboard()
    {
        Console.Clear();
        Console.WriteLine($"\nNOUGHTS AND CROSSES - SCOREBOARD\n");
        Console.WriteLine("Player                End Date/Time                    Wins  Losses  Draws");
        Console.WriteLine("--------------------  -----------------------------    ----  ------  -----");
 
        List<string> gamesListing = GameController.ScoreboardText();
        if (gamesListing.Count == 0)
            Console.WriteLine("\nThe scoreboard currently has no entries.\n");
        else foreach (string each in gamesListing)
            Console.WriteLine(each);        

        Console.WriteLine("\n"); 
        HitAnyKeyToContinue();
        Console.Clear();
    }

    public static void DisplayExitingMessage(User user)
    {
        Console.Clear();
        Console.WriteLine("\nNOUGHTS AND CROSSES");
        Console.WriteLine($"\nThank you, {user.userName}, for playing.\n");
        Console.WriteLine($"Come back soon!\n");
    }
}