public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
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

        return await _projectRepository.AddProjectAsync(project);
    }
}
