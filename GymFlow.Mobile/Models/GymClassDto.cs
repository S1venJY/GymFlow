namespace GymFlow.Mobile.Models;

public record GymClassDto(Guid Id, string Title, string TrainerName, DateTime StartTime, int MaxCapacity, int BookedCount);