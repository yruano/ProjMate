using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly string _jwtSecretKey;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IConfiguration config)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _jwtSecretKey = config["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey is missing from configuration.");
    }

    public async Task<User> RegisterAsync(RegisterRequest register)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(register.Username);
        if (existingUser != null)
            throw new Exception("Username already exists");

        var salt = _passwordHasher.GenerateSalt();
        var hashedPassword = _passwordHasher.HashPassword(register.Password, salt);

        var user = new User
        {
            Username = register.Username,
            Password = hashedPassword,
            Salt = salt,
            DiscordId = register.DiscordId,
            Position = register.Position,
        };

        await _userRepository.AddUserAsync(user);
        return user;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return null;

        bool isValid = _passwordHasher.VerifyPassword(password, user.Password, user.Salt);
        if (!isValid)
            return null;

        return GenerateJwtToken(user);
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("DiscordId", user.DiscordId)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
