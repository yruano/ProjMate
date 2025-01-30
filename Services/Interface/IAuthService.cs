public interface IAuthService
{
    Task<User> RegisterAsync(RegisterRequest register);
    Task<string> LoginAsync(string username, string password);
    string GenerateJwtToken(User user);
}
