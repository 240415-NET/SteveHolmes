using NoughtsAndCrosses.Models;
using System.Data.SqlClient;

namespace NoughtsAndCrosses.Data;

public class SqlGameStorage : IGameStorageRepo
{
    public static string connectionString = File.ReadAllText(@"C:\UserData\Steve H\ConnectionStrings\Project1ConnString.txt");

    public void StoreGame(Game game)
    {
        using SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();

            string cmdText = @"INSERT INTO dbo.Games (gameId, userId, board, startTime, endTime, winLossDraw)
                            VALUES (@gameId, @userId, @board, @startTime, @endTime, @winLossDraw);";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@gameId", game.gameId);
            cmd.Parameters.AddWithValue("@userId", game.userId);
            cmd.Parameters.AddWithValue("@board", game.board);
            cmd.Parameters.AddWithValue("@startTime", game.startTime);
            cmd.Parameters.AddWithValue("@endTime", game.endTime);
            cmd.Parameters.AddWithValue("@winLossDraw", game.winLossDraw);

            cmd.ExecuteNonQuery();
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            connection.Close();
        }
    }

    public List<Game> RetrieveGames()
    {
        List<Game> gamesList = new List<Game>();

        using SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();

            string cmdText = @"SELECT gameId, userId, board, startTime, endTime, winLossDraw 
                            FROM dbo.Games;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Game foundGame = new Game();

                foundGame.gameId = reader.GetGuid(0);
                foundGame.userId = reader.GetGuid(1);
                foundGame.board = reader.GetString(2).ToCharArray();
                foundGame.startTime = reader.GetDateTime(3);
                foundGame.endTime = reader.GetDateTime(4);
                foundGame.winLossDraw = reader.GetString(5)[0];

                gamesList.Add(foundGame);
            }

            connection.Close();

            return gamesList;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            connection.Close();
        }
        return null;

    }

    public void RemoveGames(Guid userId, Guid gameId)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();

            string cmdText = @"DELETE FROM dbo.Games
                            WHERE gameId = @gameId AND userId = @userId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@gameId", gameId);
            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            connection.Close();
        }

    }

    public void RemoveGames(Guid userId)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();

            string cmdText = @"DELETE FROM dbo.Games
                            WHERE userId = @userId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            connection.Close();
        }
    }
}
