public interface IProjectService
{
    Task<Project> CreateProjectAsync(CreateProjectRequest request, string username);
}
