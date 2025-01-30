public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByUsernameAsync(string username);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}
