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
}
