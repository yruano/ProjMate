public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectsAsync(string projectname);
}