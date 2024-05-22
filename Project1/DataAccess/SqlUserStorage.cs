using NoughtsAndCrosses.Models;
using System.Data.SqlClient;

namespace NoughtsAndCrosses.Data;

public class SqlUserStorage : IUserStorageRepo
{
    public static string connectionString = File.ReadAllText(@"C:\UserData\Steve H\ConnectionStrings\Project1ConnString.txt");

    public void StoreUser(User user)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string cmdText = @"INSERT INTO dbo.Users (userId, userName, userPassword)
                            VALUES (@userId, @userName, @userPassword);";

        using SqlCommand cmd = new SqlCommand(cmdText, connection);

        cmd.Parameters.AddWithValue("@userId", user.userId);
        cmd.Parameters.AddWithValue("@userName", user.userName);
        cmd.Parameters.AddWithValue("@userPassword", user.userPassword);

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public User FindUser(string usernameToFind)
    {   
        User foundUser = new User();

        using SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();

            string cmdText = @"SELECT userId, userName, userPassword 
                                FROM dbo.Users
                                WHERE userName=@userToFind;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@userToFind", usernameToFind);

            using SqlDataReader reader = cmd.ExecuteReader();
            
            while(reader.Read())
            {
                foundUser.userId = reader.GetGuid(0);
                foundUser.userName = reader.GetString(1);
                foundUser.userPassword = reader.GetString(2);
            }           
 
            connection.Close();

            if (foundUser.userId == Guid.Empty)
            {
                return null;
            }

            return foundUser;

        }
        catch(Exception e)
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


    public User FindUser(Guid userIdToFind)
    {
        User foundUser = new User();
        using SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();

            string cmdText = @"SELECT userId, userName, userPassword 
                                FROM dbo.Users
                                WHERE userId=@userIdToFind;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@userIdToFind", userIdToFind);

            using SqlDataReader reader = cmd.ExecuteReader();
            
            while(reader.Read())
            {
                foundUser.userId = reader.GetGuid(0);
                foundUser.userName = reader.GetString(1);
                foundUser.userPassword = reader.GetString(2);
            }           
 
            connection.Close();

            if (foundUser.userId == Guid.Empty)
            {
                return null;
            }

            return foundUser;

        }
        catch(Exception e)
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
}

