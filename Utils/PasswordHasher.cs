using System.Security.Cryptography;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    public string GenerateSalt()
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        return Convert.ToBase64String(salt);
    }

    public string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(HashSize);
        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string hashedPassword, string salt)
    {
        return HashPassword(password, salt) == hashedPassword;
    }
}
