namespace NoughtsAndCrosses.Models;

public interface IUserStorageRepo
{
    public void StoreUser(User user);
    public User FindUser(string usernameToFind);
    public User FindUser(Guid userIdToFind);

}