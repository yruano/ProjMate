public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync();
}