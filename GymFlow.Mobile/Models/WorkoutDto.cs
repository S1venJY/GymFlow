namespace GymFlow.Mobile.Models;

public class WorkoutDto
{
    public int Id { get; set; }

    public string ExerciseName { get; set; } = string.Empty;

    public string MuscleGroup { get; set; } = string.Empty;

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public int Sets { get; set; }

    public DateTime Date { get; set; }

    public string WeightText =>
        $"{Weight:0.#} кг";

    public string RepetitionsText =>
        $"{Repetitions} повторень";

    public string SetsText =>
        $"{Sets} підходи";

    public string DateText =>
        Date.ToString("dd.MM.yyyy");

    public WorkoutDto()
    {
    }

    public WorkoutDto(
        int id,
        string exerciseName,
        string muscleGroup,
        double weight,
        int repetitions,
        int sets,
        DateTime date)
    {
        Id = id;
        ExerciseName = exerciseName;
        MuscleGroup = muscleGroup;
        Weight = weight;
        Repetitions = repetitions;
        Sets = sets;
        Date = date;
    }
}