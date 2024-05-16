using NoughtsAndCrosses.Models;
using System.Text.Json;

namespace NoughtsAndCrosses.Data;

public class JsonUserStorage : IUserStorageRepo
{
    public static string filePath = "UsersFile.json";

public void StoreUser(User user)
    {          
        if(File.Exists(filePath))
        {           
            string existingUsersJson = File.ReadAllText(filePath);
            List<User> existingUsersList = JsonSerializer.Deserialize<List<User>>(existingUsersJson);           
            existingUsersList.Add(user);
            string jsonExistingUsersListString = JsonSerializer.Serialize(existingUsersList);
            File.WriteAllText(filePath, jsonExistingUsersListString);
        }
        else if (!File.Exists(filePath)) // file will not exist the first time we run this
        {
            List<User> initialUsersList = new List<User>();
            initialUsersList.Add(user);
            string jsonUsersListString = JsonSerializer.Serialize(initialUsersList);
            File.WriteAllText(filePath, jsonUsersListString);
        }
    }

    public User FindUser(string usernameToFind)
    {
        if (!File.Exists(filePath)) return null; // will not exist the first time we run this
        
        User foundUser = new User();
        try{
            string existingUsersJson = File.ReadAllText(filePath);
            List<User> existingUsersList = JsonSerializer.Deserialize<List<User>>(existingUsersJson);
            foundUser = existingUsersList.FirstOrDefault(user => user.userName == usernameToFind);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }       
        return foundUser;
    }

    public User FindUser(Guid userIdToFind)
    {
        if (!File.Exists(filePath)) return null; // will not exist the first time we run this
        
        User foundUser = new User();
        try{
            string existingUsersJson = File.ReadAllText(filePath);
            List<User> existingUsersList = JsonSerializer.Deserialize<List<User>>(existingUsersJson);
            foundUser = existingUsersList.FirstOrDefault(user => user.userId == userIdToFind);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }       
        return foundUser;
    }
}