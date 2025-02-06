using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class CreateProjectRequest
{
    public required string Projectname { get; set; }
    public required string MaxMember { get; set; }
    public required string Category { get; set; }
    public required string Text { get; set; }
}

[ApiController]
[Route("api/[controller]")]
[Authorize] // 🔐 로그인 필수
public class AuthorizeProjectsController : ControllerBase
{
    private readonly IAuthorizeProjectService _authorizeProjectService;

    public AuthorizeProjectsController(IAuthorizeProjectService authorizeProjectService)
    {
        _authorizeProjectService = authorizeProjectService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        try
        {
            // 현재 로그인한 사용자명 추출
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var project = await _authorizeProjectService.CreateProjectAsync(request, username);
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
