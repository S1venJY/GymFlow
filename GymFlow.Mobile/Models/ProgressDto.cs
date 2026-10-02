namespace GymFlow.Mobile.Models;

public class ProgressDto
{
    public int WorkoutsCount { get; set; }

    public double TotalVolume { get; set; }

    public double MaxWeight { get; set; }

    public int CurrentStreak { get; set; }

    public string WorkoutsCountText =>
        WorkoutsCount.ToString();

    public string TotalVolumeText =>
        $"{TotalVolume:0.#} кг";

    public string MaxWeightText =>
        $"{MaxWeight:0.#} кг";

    public string CurrentStreakText =>
        $"{CurrentStreak} дн.";
}