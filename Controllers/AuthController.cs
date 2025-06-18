using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var token = await _authService.AuthenticateAsync(login.Email, login.Password);
        if (token == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        return Ok(new { token });
    }
}

public record LoginDto(string Email, string Password);
