public class AuthorizeProjectService : IAuthorizeProjectService
{
    private readonly IProjectRepository _projectRepository;

    public AuthorizeProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project> CreateProjectAsync(CreateProjectRequest request, string username)
    {
        var project = new Project
        {
            Projectname = request.Projectname,
            Username = username,
            MaxMember = request.MaxMember,
            Category = request.Category,
            Text = request.Text
        };

        await _projectRepository.AddProjectAsync(project);
        return project;
    }

    public async Task<string> DeleteProjectAsync(string projectname, string username)
    {
        await _projectRepository.DeleteProjectAsync(projectname, username);
        return projectname;
    }
}
