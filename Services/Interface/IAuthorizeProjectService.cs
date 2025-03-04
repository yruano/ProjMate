public interface IAuthorizeProjectService
{
    Task<Project> CreateProjectAsync(CreateProjectRequest request, string username);
    Task<string> DeleteProjectAsync(string projectname, string username);
}
