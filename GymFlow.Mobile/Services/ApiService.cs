using System.Net.Http.Json;
using GymFlow.Mobile.Models;

namespace GymFlow.Mobile.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/")
        };

        _httpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<List<GymClassDto>?> GetGymClassesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/classes");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<List<GymClassDto>>();
            }

            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"GetGymClassesAsync error: {ex.Message}");

            return null;
        }
    }


    public async Task<bool> BookClassAsync(int classId)
    {
        try
        {
            var response = await _httpClient.PostAsync(
                $"api/classes/{classId}/book",
                null);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"BookClassAsync error: {ex.Message}");

            return false;
        }
    }


    public async Task<List<WorkoutDto>?> GetWorkoutsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(
                "api/workouts");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<List<WorkoutDto>>();
            }

            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"GetWorkoutsAsync error: {ex.Message}");

            return null;
        }
    }

    public async Task<bool> AddWorkoutAsync(
        WorkoutDto workout)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/workouts",
                workout);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"AddWorkoutAsync error: {ex.Message}");

            return false;
        }
    }


    public async Task<ProgressDto?> GetProgressAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(
                "api/progress");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<ProgressDto>();
            }

            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"GetProgressAsync error: {ex.Message}");

            return null;
        }
    }


    public async Task<ProfileDto?> GetProfileAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(
                "api/profile");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<ProfileDto>();
            }

            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"GetProfileAsync error: {ex.Message}");

            return null;
        }
    }


    public List<GymClassDto> GetDemoClasses()
    {
        return new List<GymClassDto>
        {
            new GymClassDto(
                1,
                "CrossFit Intense",
                "Олександр Коваль",
                DateTime.Now.AddHours(2),
                15,
                5),

            new GymClassDto(
                2,
                "Yoga & Balance",
                "Олена Бойко",
                DateTime.Now.AddDays(1),
                12,
                3),

            new GymClassDto(
                3,
                "Power Lifting",
                "Дмитро Сидоренко",
                DateTime.Now.AddDays(1).AddHours(4),
                10,
                2),

            new GymClassDto(
                4,
                "Functional Training",
                "Андрій Мельник",
                DateTime.Now.AddDays(2),
                20,
                8)
        };
    }

    public List<WorkoutDto> GetDemoWorkouts()
    {
        return new List<WorkoutDto>
        {
            new WorkoutDto(
                1,
                "Жим лежачи",
                "Груди",
                80,
                8,
                4,
                DateTime.Now.AddDays(-1)),

            new WorkoutDto(
                2,
                "Присідання",
                "Ноги",
                100,
                6,
                4,
                DateTime.Now.AddDays(-2)),

            new WorkoutDto(
                3,
                "Станова тяга",
                "Спина",
                120,
                5,
                3,
                DateTime.Now.AddDays(-4)),

            new WorkoutDto(
                4,
                "Підйом гантелей",
                "Біцепс",
                18,
                12,
                3,
                DateTime.Now.AddDays(-5))
        };
    }

    public ProgressDto GetDemoProgress()
    {
        return new ProgressDto
        {
            WorkoutsCount = 18,
            TotalVolume = 8240,
            MaxWeight = 120,
            CurrentStreak = 7
        };
    }

    public ProfileDto GetDemoProfile()
    {
        return new ProfileDto(
            "Ярослав Запорожець",
            "yaroslav@example.com",
            "Premium",
            8,
            20,
            DateTime.Now.AddMonths(2));
    }
}