using System.Runtime.CompilerServices;
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
 
            Console.WriteLine($"Welcome {user.userName}.\n\nPlease select one of the following options:\n");
            Console.WriteLine("1. Start a new game");
            Console.WriteLine("2. Continue playing a previous game");
            Console.WriteLine("3. Review game history");
            Console.WriteLine("4. Clear game history");
            Console.WriteLine("5. View the scoreboard");
            Console.WriteLine("6. Exit\n");


            try
            {
                Console.Write(": ");
                userChoice = Convert.ToInt32(Console.ReadLine());
                validInput = true;

                switch (userChoice)
                {
                    case 1: // start a new game
                        StartNewGame(user);
                        break;
                    case 2: // continue playing a previous game
                        Console.WriteLine(">>>>>> Continue playing a previous game");
                        HitAnyKeyToContinue();
                        Console.Clear();
                        break;
                    case 3: // review game history   
                        ReviewGameHistory(user);
                        break;
                    case 4: // clear game history                      
                        Console.WriteLine(">>>>>> Clear game history");
                        HitAnyKeyToContinue();
                        Console.Clear();
                        break;
                    case 5: // view the scoreboard                     
                        ViewScoreboard();
                        break;
                    case 6: //exit                     
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

    public static void StartNewGame(User user)
    {
        Game newGame = new Game(user);
        PlayGame.StartGame(user, newGame);

        // at the end of the game we save it

        GameController.SaveGame(newGame);
        
        HitAnyKeyToContinue();
        Console.Clear();
    }

    public static void ReviewGameHistory(User user)
    {
        Console.Clear();
        Console.WriteLine($"\nNOUGHTS AND CROSSES - Game History for {user.userName}\n");
        Console.WriteLine("Start Date/Time                 End Date/Time                   Win/Loss/Draw");
        Console.WriteLine("---------------                 -------------                   -------------");

        List<string> gamesListing = GameController.AllGamesAsText(user);
        foreach (string each in gamesListing)
            Console.WriteLine(each);
        
        HitAnyKeyToContinue();
        Console.Clear();
    }    

    public static void ViewScoreboard()
    {
        Console.Clear();
        Console.WriteLine($"\nNOUGHTS AND CROSSES - SCOREBOARD\n");
        List<string> gamesListing = GameController.AllGamesAsText();
        foreach (string each in gamesListing)
            Console.WriteLine(each);
            
        HitAnyKeyToContinue();
        Console.Clear();
    }

}