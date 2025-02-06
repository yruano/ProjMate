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
            "INSERT INTO projects (projectname, username, maxmember, category, text) " +
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

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        var projects = new List<Project>();

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM projects", connection);
        await using var reader = await cmd.ExecuteReaderAsync();

        // 모든 행 순회
        while (await reader.ReadAsync())
        {
            projects.Add(new Project
            {
                Id = reader.GetInt64(0),
                Projectname = reader.GetString(1),
                Username = reader.GetString(2),
                MaxMember = reader.GetString(3),
                Category = reader.GetString(4),
                Text = reader.GetString(5)
            });
        }

        return projects;
    }
}
