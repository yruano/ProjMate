public interface IProjectRepository
{
    Task AddProjectAsync(Project project);
    Task<List<Project>> GetAllProjectsAsync();
}
