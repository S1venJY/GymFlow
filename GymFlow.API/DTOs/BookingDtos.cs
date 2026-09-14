namespace GymFlow.API.DTOs;

public record CreateBookingDto(Guid GymClassId);
public record GymClassDto(Guid Id, string Title, string TrainerName, DateTime StartTime, int MaxCapacity, int BookedCount);