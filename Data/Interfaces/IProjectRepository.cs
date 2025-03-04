public interface IProjectRepository
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectAsync(string projectname);
    Task AddProjectAsync(Project project);
    Task DeleteProjectAsync(string projectname, string username);
}
