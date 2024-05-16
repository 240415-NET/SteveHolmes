namespace NoughtsAndCrosses.Models;

public interface IGameStorageRepo
{
    public void StoreGame(Game game);
    public List<Game> RetrieveGames();

}