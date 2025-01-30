using Npgsql;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("PostgreSQL");
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = new NpgsqlCommand(
            "SELECT * FROM users WHERE id = @id",
            connection
        );
        cmd.Parameters.AddWithValue("@id", id);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2),
                Salt = reader.GetString(3),
                DiscordId = reader.GetString(4)
            };
        }
        return null;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        await using var cmd = new NpgsqlCommand(
            "SELECT * FROM users WHERE username = @username",
            connection
        );
        cmd.Parameters.AddWithValue("@username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2),
                Salt = reader.GetString(3),
                DiscordId = reader.GetString(4)
            };
        }
        return null;
    }

    public async Task AddUserAsync(User user)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        await using var cmd = new NpgsqlCommand(
            "INSERT INTO users (username, password, salt, discordid) " +
            "VALUES (@username, @password, @salt, @discordid)",
            connection
        );
        cmd.Parameters.AddWithValue("@username", user.Username);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@salt", user.Salt);
        cmd.Parameters.AddWithValue("@discordid", user.DiscordId);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        await using var cmd = new NpgsqlCommand(
            "UPDATE users SET " +
            "username = @username, " +
            "password = @password, " +
            "salt = @salt, " +
            "discordid = @discordid " +
            "WHERE id = @id",
            connection
        );
        cmd.Parameters.AddWithValue("@id", user.Id);
        cmd.Parameters.AddWithValue("@username", user.Username);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@salt", user.Salt);
        cmd.Parameters.AddWithValue("@discordid", user.DiscordId);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        await using var cmd = new NpgsqlCommand(
            "DELETE FROM users WHERE id = @id",
            connection
        );
        cmd.Parameters.AddWithValue("@id", id);
        await cmd.ExecuteNonQueryAsync();
    }
}
