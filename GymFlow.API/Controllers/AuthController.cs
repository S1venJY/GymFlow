using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymFlow.API.Data;
using GymFlow.API.Entities;
using GymFlow.API.DTOs;

namespace GymFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
            return BadRequest(new { message = "Некоректний формат Email." });

        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest(new { message = "Користувач з таким Email вже існує." });

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Реєстрація успішна!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Невірний Email або пароль." });

        return Ok(new AuthResponseDto("jwt-token-sample-xyz", user.FullName, user.Role));
    }

    [HttpGet("profile/{userId}")]
    public async Task<IActionResult> GetProfile(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound(new { message = "Користувача не знайдено." });

        return Ok(new { user.Id, user.FullName, user.Email, user.Role, user.CreatedAt });
    }
}