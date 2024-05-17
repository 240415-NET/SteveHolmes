using System.Runtime.CompilerServices;
using NoughtsAndCrosses.Controllers;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Presentation;

public class PlayGame 
{
    // user interaction for playing a game

    public static void StartGame(User user, Game game)
    {
        PlayGameController.StartGame(user, game);

        // placeholder for game playing user interaction
        Console.WriteLine("\nFor now, let's just pretend that we have started to play a game.\n");

    }


}
