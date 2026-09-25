using System.Net.Http.Json;
using GymFlow.Mobile.Models;

namespace GymFlow.Mobile.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    // Вказуємо адресу нашого API
    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5094/")
        };
    }

    public async Task<List<GymClassDto>?> GetClassesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<GymClassDto>>("api/Bookings/classes");
        }
        catch
        {
            return new List<GymClassDto>();
        }
    }

    public async Task<bool> BookClassAsync(Guid classId, Guid userId)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Bookings/book?userId={userId}", new { gymClassId = classId });
        return response.IsSuccessStatusCode;
    }
}