using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymFlow.API.Data;
using GymFlow.API.Entities;
using GymFlow.API.DTOs;

namespace GymFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("classes")]
    public async Task<IActionResult> GetClasses()
    {
        var classes = await _context.GymClasses
            .Select(c => new GymClassDto(c.Id, c.Title, c.TrainerName, c.StartTime, c.MaxCapacity, c.BookedCount))
            .ToListAsync();

        return Ok(classes);
    }

    [HttpPost("book")]
    public async Task<IActionResult> BookClass([FromBody] CreateBookingDto dto, [FromQuery] Guid userId)
    {
        var gymClass = await _context.GymClasses.FindAsync(dto.GymClassId);
        if (gymClass == null)
            return NotFound(new { message = "Заняття не знайдено." });

        if (gymClass.BookedCount >= gymClass.MaxCapacity)
            return BadRequest(new { message = "На це тренування немає вільних місць." });

        var booking = new Booking
        {
            UserId = userId,
            GymClassId = dto.GymClassId
        };

        gymClass.BookedCount++;
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Запис успішно створено!" });
    }
}