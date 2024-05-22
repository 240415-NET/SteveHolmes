using NoughtsAndCrosses.Data;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Controllers;

public class GameController
{

    private static IGameStorageRepo _gameData = new SqlGameStorage();

    public static void SaveGame(Game game) 
    {
        _gameData.StoreGame(game);
    }

    public static void UpdateGame(Game game)
    {
        _gameData.RemoveGames(game.userId, game.gameId);
        _gameData.StoreGame(game);
    }

    public static List<Game> SavedGames() 
    {
       return _gameData.RetrieveGames();
    }

    public static List<string> AllGamesAsText()
    {
        List<Game> gamesList = SavedGames();
        List<string> gamesListing = new();

        for (int i = gamesList.Count() - 1; i >= 0; i--)
        {
            Game each = gamesList.ElementAt(i);
            string userName = UserController.FindUser(each.userId).userName;
            gamesListing.Add($"{userName,-16} {DateTimeString(each.startTime)}    {DateTimeString(each.endTime)}          {each.winLossDraw}");
        }

        return gamesListing;
    }

    public static List<string> AllGamesAsText(User user)
    {   
        List<string> gamesListing = new();
        var gamesToDisplay = SavedGames().Where(x => x.userId == user.userId);
        var sortedGames = gamesToDisplay.OrderByDescending(e => e.startTime);

        foreach (Game eachGame in sortedGames)
        {
            string endTimeDisplay = eachGame.isInProgress() ? "In Progress                 " : DateTimeString(eachGame.endTime);
            gamesListing.Add($"{DateTimeString(eachGame.startTime)}       {endTimeDisplay}             {eachGame.winLossDraw}");
        }

        return gamesListing;
    }

    public static List<string> ScoreboardText()
    {
        List<string> scoreboardListing = new();

        var gamesToDisplay = SavedGames().Where( x => !x.isInProgress());
        var sortedGames = gamesToDisplay.OrderByDescending( e => e.endTime);
        var userIds = sortedGames.Select( x => x.userId);
        var distinctUserIds = userIds.Distinct();

        foreach (Guid eachUserId in distinctUserIds)
        {
            int wins = sortedGames.Count(x => x.userId == eachUserId && x.winLossDraw == 'W');
            int losses = sortedGames.Count(x => x.userId == eachUserId && x.winLossDraw == 'L');
            int draws = sortedGames.Count(x => x.userId == eachUserId && x.winLossDraw == 'D');

            Game mostRecentGame = sortedGames.First(x => x.userId == eachUserId);
            string userName = UserController.FindUser(eachUserId).userName;
            scoreboardListing.Add($"{userName,-16} {DateTimeString(mostRecentGame.endTime)}      {wins,3}     {losses,3}    {draws,3}");
        }
        return scoreboardListing;
    }


    public static List<string> NumberedGamesInProgress(User user)
    {   
        List<string> gamesListing = new();
        var gamesToDisplay = SavedGames().Where( x => x.userId == user.userId && x.isInProgress());
        var sortedGames = gamesToDisplay.OrderByDescending( e => e.startTime);

        int counter = 0;
        foreach(Game eachGame in sortedGames)                    
            gamesListing.Add($"{++counter}    {DateTimeString(eachGame.startTime)}");
        
        return gamesListing;
    }

    public static Game NumberedGameAt(User user, int gameNumber)
    {   
        List<string> gamesListing = new();
        var gamesToDisplay = SavedGames().Where( x => x.userId == user.userId && x.isInProgress());
        var sortedGames = gamesToDisplay.OrderByDescending( e => e.startTime);

        Game game = sortedGames.ElementAt(gameNumber);
        return game;
    }

    public static void ClearGameHistory(User user)
    {
        _gameData.RemoveGames(user.userId);
    }

    public static DateTime DateAndTimeNow()
    {
        DateTime dtmUtc = DateTime.UtcNow;
        TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(dtmUtc, easternTimeZone);
        return easternTime;
    }

    public static string DateTimeString(DateTime dtm)
    {
        return $"{dtm.ToShortDateString(),-10}{dtm.DayOfWeek.ToString().Trim(),-10}{dtm.ToShortTimeString(),-8}";
    }

    public static int NumberFromUser()
    // prompt for and return a number from the user. ensure that we have a number to return.
    {
        int userNumber = 0;
        bool userInputIsValid = true;
        do
        {
            if (userInputIsValid == false)
            {
                Console.Write("Invalid Entry. Please enter a number: ");
            }
            string userString = Console.ReadLine();
            userInputIsValid = true;
            try
            {
                userNumber = Convert.ToInt32(userString);
            }
            catch (Exception e)
            {
                userInputIsValid = false;
            }
        }
        while (userInputIsValid != true);
        return userNumber;
    }
}