using NoughtsAndCrosses.Data;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Controllers;

public class PlayGameController
{
    public static void RegisterStartOfGame(User user, Game game)
    {
       if (game.startTime == Game.MinimumSqlDateTime())
            game.startTime = GameController.DateAndTimeNow();
    }

    public static void RegisterEndOfGame(User user, Game game)
    {
        game.endTime = GameController.DateAndTimeNow();
    }

    public static List<string> DisplayableBoardRows(Game game)
    {
        List<string> boardListing = new();
        char[] board = game.board;

        boardListing.Add("          |          |          ");
        boardListing.Add($"    {board[0]}     |    {board[1]}     |     {board[2]}    ");
        boardListing.Add("__________|__________|__________");
        boardListing.Add("          |          |          " );
        boardListing.Add($"    {board[3]}     |    {board[4]}     |     {board[5]}    ");
        boardListing.Add("__________|__________|__________");
        boardListing.Add("          |          |          ");
        boardListing.Add($"    {board[6]}     |    {board[7]}     |     {board[8]}    ");
        boardListing.Add("          |          |          ");

        return boardListing;        
    }

    public static bool IsPositionAvailable(Game game, int n)
    {
        return game.IsPositionAvailable(n);
    }

    public static void UserSelectsPosition(Game game, int n)
    {
        game.UserSelectsPosition(n);
    }

    public static bool DidUserWin(Game game)
    {
        return game.DidUserWin();
    }

    public static bool DidUserLose(Game game)
    {
        return game.DidUserLose();
    }

    public static bool DidUserDraw(Game game)
    {
        return game.DidUserDraw();
    }

    public static bool HasGameFinished(Game game)
    {
        return DidUserWin(game) || DidUserLose(game) || DidUserDraw(game);
    }

    public static void RecordUserWin(Game game)
    {
        game.RecordUserWin();
    }

    public static void RecordUserLoss(Game game)
    {
        game.RecordUserLoss();
    }

    public static void RecordUserDraw(Game game)
    {
        game.RecordUserDraw();
    }

} 