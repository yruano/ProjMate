using Microsoft.AspNetCore.Mvc;

public class RegisterRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string DiscordId { get; set; }
}

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var user = await _authService.RegisterAsync(request);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _authService.LoginAsync(request.Username, request.Password);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}

