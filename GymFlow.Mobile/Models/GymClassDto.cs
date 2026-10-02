namespace GymFlow.Mobile.Models;

public class GymClassDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string TrainerName { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public int Capacity { get; set; }

    public int AvailablePlaces { get; set; }

    public string DateText =>
        StartTime.ToString("dd.MM.yyyy");

    public string TimeText =>
        StartTime.ToString("HH:mm");

    public string PlacesText =>
        $"Вільних місць: {AvailablePlaces}/{Capacity}";

    public GymClassDto()
    {
    }

    public GymClassDto(
        int id,
        string name,
        string trainerName,
        DateTime startTime,
        int capacity,
        int availablePlaces)
    {
        Id = id;
        Name = name;
        TrainerName = trainerName;
        StartTime = startTime;
        Capacity = capacity;
        AvailablePlaces = availablePlaces;
    }
}