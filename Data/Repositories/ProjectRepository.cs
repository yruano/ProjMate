using Npgsql;

public class ProjectRepository : IProjectRepository
{
    private readonly string _connectionString;

    public ProjectRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("PostgreSQL");
    }

    public async Task AddProjectAsync(Project project)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var cmd = new NpgsqlCommand(
            "INSERT INTO project (projectname, username, maxmember, category, text) " +
            "VALUES (@projectname, @username, @maxmember, @category, @text)",
            connection
        );
        cmd.Parameters.AddWithValue("@projectname", project.Projectname);
        cmd.Parameters.AddWithValue("@username", project.Username);
        cmd.Parameters.AddWithValue("@maxmember", project.MaxMember);
        cmd.Parameters.AddWithValue("@category", project.Category);
        cmd.Parameters.AddWithValue("@text", project.Text);
        await cmd.ExecuteNonQueryAsync();
    }
}
