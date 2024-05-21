using NoughtsAndCrosses.Models;
using System.Text.Json;

namespace NoughtsAndCrosses.Data;

public class JsonGameStorage : IGameStorageRepo
{
    public static string filePath = "GamesFile.json";

    public void StoreGame(Game game)
    {

        if (File.Exists(filePath))
        {
            string existingGamesJson = File.ReadAllText(filePath);
            List<Game> existingGamesList = JsonSerializer.Deserialize<List<Game>>(existingGamesJson);
            existingGamesList.Add(game);
            string jsonUpdatedGamesListString = JsonSerializer.Serialize(existingGamesList);
            File.WriteAllText(filePath, jsonUpdatedGamesListString);
        }
        else if (!File.Exists(filePath)) // initially the file will not exist
        {
            List<Game> initialGamesList = new List<Game>();
            initialGamesList.Add(game);
            string jsonGamesListString = JsonSerializer.Serialize(initialGamesList);
            File.WriteAllText(filePath, jsonGamesListString);
        }

    }


    public List<Game> RetrieveGames()
    {
        List<Game> gamesList = new List<Game>();

        if (File.Exists(filePath))
        {
            string existingGamesJson = File.ReadAllText(filePath);
            gamesList = JsonSerializer.Deserialize<List<Game>>(existingGamesJson);
        }

        return gamesList;
    }    
    
    public void RemoveGames(Guid userId, Guid gameId)
    {
        List<Game> allGames = new List<Game>();

        if (File.Exists(filePath))
        {
            string existingGamesJson = File.ReadAllText(filePath);
            allGames = JsonSerializer.Deserialize<List<Game>>(existingGamesJson);
            allGames.RemoveAll(each => each.userId == userId && each.gameId == gameId);

            string jsonUpdatedGamesListString = JsonSerializer.Serialize(allGames);
            File.WriteAllText(filePath, jsonUpdatedGamesListString);
        }
    }

    public void RemoveGames(Guid userId)
    {
        List<Game> allGames = new List<Game>();

        if (File.Exists(filePath))
        {
            string existingGamesJson = File.ReadAllText(filePath);
            allGames = JsonSerializer.Deserialize<List<Game>>(existingGamesJson);
            allGames.RemoveAll(each => each.userId == userId);

            string jsonUpdatedGamesListString = JsonSerializer.Serialize(allGames);
            File.WriteAllText(filePath, jsonUpdatedGamesListString);
        }
    }
}