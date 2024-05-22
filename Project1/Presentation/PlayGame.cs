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
                userPlay = GameController.NumberFromUser();

                if (userPlay == 0)
                {
                    shouldSaveGame = true;
                    isValidInput = true;
                    Console.Write("\nCurrent game is being saved.\n");
                }
                else if (userPlay < 0 || userPlay > 9)
                {
                    Console.Write("Invalid entry. Enter a number for the box you want to choose: ");
                    isValidInput = false;
                }
                isValidInput = true;
                
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
                Console.WriteLine("\n\nCongratulations, You Win!");
                PlayGameController.RecordUserWin(game);
            }

        if (PlayGameController.DidUserLose(game))
            {   
                Console.WriteLine("\n\nCommiserations, You Lose!");
                PlayGameController.RecordUserLoss(game);
            }

        if (PlayGameController.DidUserDraw(game)) 
            {
                Console.WriteLine("\n\nBetter luck next time. It was a draw!");
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

        Console.Write("\nPlease wait. System is deciding next play.");
        for (int i=0; i<5; i++) {Thread.Sleep(500); Console.Write(".");}

        // system "plays" by taking the next available position, if there is one
        int nextPlay = game.FirstAvailablePosition();
        if (nextPlay >= 0)
            game.SystemSelectsPosition(nextPlay);
        
    }

}
