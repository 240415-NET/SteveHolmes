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
            string jsonExistingGamesListString = JsonSerializer.Serialize(existingGamesList);
            File.WriteAllText(filePath, jsonExistingGamesListString);
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
    
}