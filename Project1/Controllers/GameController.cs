using NoughtsAndCrosses.Data;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Controllers;

public class GameController
{

    private static IGameStorageRepo _gameData = new JsonGameStorage();

    public static void SaveGame(Game game)   // store a single game
    {
        _gameData.StoreGame(game);
    }

    public static List<Game> SavedGames()    // retrieve a list of all games
    {
        return _gameData.RetrieveGames();
    }

    public static List<string> AllGamesAsText()
    {
        List<Game> gamesList = SavedGames();
        List<string> gamesListing = new();
        foreach (Game each in gamesList)
        {
            string userName = UserController.FindUser(each.userId).userName;
            gamesListing.Add($"{userName.PadRight(20)}{each}");
        }
        return gamesListing;
    }

    public static List<string> AllGamesAsText(User user)
    {   
        List<string> gamesListing = new();
        var gamesToDisplay = SavedGames().Where( x => x.userId == user.userId);

        foreach (Game each in gamesToDisplay)
            gamesListing.Add($"{DateTimeString(each.startTime)}    {DateTimeString(each.endTime)}          {each.winLossDraw}");
    
        return gamesListing;
    }

    public static DateTime DateAndTimeNow()
    {
        // get current date & time (utc)
        DateTime dtmUtc = DateTime.UtcNow;

        // need local time zone; assuming eastern time
        TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        // apply time zone to get local date & time
        DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(dtmUtc, easternTimeZone);

        return easternTime;
    }

    public static string DateTimeString(DateTime dtm)
    {
        return $"{dtm.ToShortDateString(),-10}{dtm.DayOfWeek.ToString().Trim(),-10}{dtm.ToShortTimeString(),-8}";
    }
}