namespace GymFlow.Mobile.Models;

public class ProfileDto
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string SubscriptionName { get; set; } = string.Empty;

    public int RemainingVisits { get; set; }

    public int TotalVisits { get; set; }

    public DateTime SubscriptionEndDate { get; set; }

    public string SubscriptionEndDateText =>
        SubscriptionEndDate.ToString("dd.MM.yyyy");

    public string VisitsText =>
        $"{RemainingVisits} з {TotalVisits}";

    public ProfileDto()
    {
    }

    public ProfileDto(
        string fullName,
        string email,
        string subscriptionName,
        int remainingVisits,
        int totalVisits,
        DateTime subscriptionEndDate)
    {
        FullName = fullName;
        Email = email;
        SubscriptionName = subscriptionName;
        RemainingVisits = remainingVisits;
        TotalVisits = totalVisits;
        SubscriptionEndDate = subscriptionEndDate;
    }
}