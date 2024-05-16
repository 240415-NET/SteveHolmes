using NoughtsAndCrosses.Data;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Controllers;

public class PlayGameController
{
    // placeholder for game logic

    public static void StartGame(User user, Game game)
    {
        game.startTime = GameController.DateAndTimeNow();


    }

}