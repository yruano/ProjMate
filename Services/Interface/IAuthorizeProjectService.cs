public interface IAuthorizeProjectService
{
    Task<Project> CreateProjectAsync(CreateProjectRequest request, string username);
}
