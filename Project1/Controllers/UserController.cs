using System.Security.Cryptography;
using System.Text;
using NoughtsAndCrosses.Data;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Controllers;

public class UserController
{

    private static IUserStorageRepo _userData = new SqlUserStorage();

    public static User CreateUser(string userName)
        {
            User newUser = new User(userName);
            _userData.StoreUser(newUser);
            return newUser;
        }

    public static User CreateUser(string userName, string userPassword)
        {
            User newUser = new User(userName);
            newUser.userPassword = GetEncodedStringFor(newUser.userId, userPassword);
            _userData.StoreUser(newUser);
            return newUser;
        }

    public static bool UserExists(string userName)
    {       
        if(_userData.FindUser(userName) != null)
            return true;

        return false;
    }

    public static User ReturnUser(string userName)
    {
        User existingUser = _userData.FindUser(userName);
        return existingUser;
    }

    public static User FindUser(Guid userId)
    {
        User existingUser = _userData.FindUser(userId);
        return existingUser;
    }

    public static string GetEncodedStringFor(Guid userId, string password)
    //
    // return an encoded string for the supplied userId and password.
    // use the userId as a "salt" to combine with the password.
    // create a SHA512 hash digest, return as a hex string.
    //
    {
        string stringToEncode = userId + password;
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(stringToEncode);
        byte[] hashDigest = SHA512.HashData(bytesToEncode);

        return Convert.ToHexString(hashDigest);
    }

    public static bool IsUserPasswordCorrect(User user, string password)
    {
        string encodedPassword = GetEncodedStringFor(user.userId, password);
        return encodedPassword == user.userPassword;
    }

}