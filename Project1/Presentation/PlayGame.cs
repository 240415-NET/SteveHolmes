using System.Runtime.CompilerServices;
using NoughtsAndCrosses.Controllers;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Presentation;

public class PlayGame
{
    // user interaction for playing a game

    public static void Play(User user, Game game)
    {
        PlayGameController.RegisterStartOfGame(user, game);

        bool hasGameFinished = true;
        bool isValidInput = true;
        bool shouldSaveGame = false;
        int userPlay = 0;

        DisplayGameBoard(user, game);

        do
        {
            DisplayGameBoard(user, game);
            Console.Write("\nEnter the number for the box you want to play (or 0 to save the game for later): ");
            do
            {
                userPlay = Convert.ToInt32(Console.ReadLine());

                if (userPlay == 0)
                {
                    shouldSaveGame = true;
                    isValidInput = true;
                    Console.Write("Current game is being saved. ");
                }
                else if (userPlay < 0 || userPlay > 9)
                {
                    Console.WriteLine("Invalid entry. Enter a number for the box you want to choose.");
                    isValidInput = false;
                }
            } while (!isValidInput);

            if (!shouldSaveGame)
            {
                PlayGameController.UserSelectsPosition(game, userPlay - 1);
                hasGameFinished = PlayGameController.HasGameFinished(game);
                if (!hasGameFinished)
                    {
                        SystemPlay(user, game);
                        hasGameFinished = PlayGameController.HasGameFinished(game);
                    }
            }

        } while (!hasGameFinished && !shouldSaveGame);

        if (!shouldSaveGame)
        {
            DisplayGameBoard(user, game);
            ShowResults(game);
            PlayGameController.RegisterEndOfGame(user, game);
        }
    }

    public static void ShowResults(Game game)
    {
       if (PlayGameController.DidUserWin(game)) 
            {
                Console.WriteLine("Congratulations, You Win!");
                PlayGameController.RecordUserWin(game);
            }

        if (PlayGameController.DidUserLose(game))
            {   
                Console.WriteLine("Commiserations, You Lose!");
                PlayGameController.RecordUserLoss(game);
            }

        if (PlayGameController.DidUserDraw(game)) 
            {
                Console.WriteLine("Better luck next time. It was a draw!");
                PlayGameController.RecordUserDraw(game);
            }

    }

    public static void DisplayGameBoard(User user, Game game)
    {
        Console.Clear();
        Console.WriteLine($"\nNOUGHTS AND CROSSES - {user.userName} is X, the system is O\n\n");

        foreach (string each in PlayGameController.DisplayableBoardRows(game))
            Console.WriteLine($"\t{each}");

        Console.WriteLine();

    }

    public static void SystemPlay(User user, Game game)
    {
        DisplayGameBoard(user, game);
        Console.Write("\nPlease wait. System is deciding next play...");
        Thread.Sleep(3000);
        int nextPlay = game.FirstAvailablePosition();
        if (nextPlay >= 0)
            game.SystemSelectsPosition(nextPlay);
        
    }

}
