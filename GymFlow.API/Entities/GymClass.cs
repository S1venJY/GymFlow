namespace GymFlow.API.Entities;

public class GymClass
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string TrainerName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int MaxCapacity { get; set; }
    public int BookedCount { get; set; } = 0;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}