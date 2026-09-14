namespace GymFlow.API.Entities;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid GymClassId { get; set; }
    public GymClass? GymClass { get; set; }

    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Active";
}