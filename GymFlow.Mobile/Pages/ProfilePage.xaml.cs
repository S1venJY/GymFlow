using GymFlow.Mobile.Models;
using GymFlow.Mobile.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFlow.Mobile.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly ApiService _apiService;

    public ProfilePage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        UpdateAuthStatus();
        await LoadDataAsync();
    }

    private void UpdateAuthStatus()
    {
        bool isLoggedIn = Preferences.Get("IsLoggedIn", false);
        string email = Preferences.Get("UserEmail", string.Empty);

        if (isLoggedIn && !string.IsNullOrEmpty(email))
        {
            EmailLabel.Text = email;
            AuthActionButton.Text = "Змінити акаунт";
        }
        else
        {
            EmailLabel.Text = "Не авторизовано";
            AuthActionButton.Text = "Увійти в акаунт";
        }
    }

    private async Task LoadDataAsync()
    {
        LoadingBlock.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        ProfileContent.IsVisible = false;

        try
        {
            var profile = await _apiService.GetProfileAsync() ?? _apiService.GetDemoProfile();
            ShowProfile(profile);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            ShowProfile(_apiService.GetDemoProfile());
        }
    }

    private void ShowProfile(ProfileDto profile)
    {
        NameLabel.Text = profile.FullName;
        if (!Preferences.Get("IsLoggedIn", false))
        {
            EmailLabel.Text = profile.Email;
        }

        SubscriptionNameLabel.Text = $"Абонемент: {profile.SubscriptionName}";
        RemainingVisitsLabel.Text = profile.VisitsText;
        SubscriptionEndDateLabel.Text = profile.SubscriptionEndDateText;

        var myBookings = new List<BookingItem>
        {
            new BookingItem { WorkoutName = "Силове тренування", DateText = "Завтра о 18:00" },
            new BookingItem { WorkoutName = "Кардіо + Планка", DateText = "П'ятниця о 19:30" }
        };

        BookingsCollectionView.ItemsSource = myBookings;

        LoadingBlock.IsVisible = false;
        LoadingIndicator.IsRunning = false;
        ProfileContent.IsVisible = true;
    }

    private async void OnAuthActionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Сповіщення", "Push-сповіщення увімкнено.", "OK");
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Налаштування", "Розділ налаштувань профілю.", "OK");
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Вихід", "Ви дійсно хочете вийти?", "Так", "Ні");

        if (result)
        {
            Preferences.Remove("UserEmail");
            Preferences.Set("IsLoggedIn", false);
            UpdateAuthStatus();
            await DisplayAlert("GymFlow", "Ви вийшли з акаунта.", "OK");
        }
    }
}

public class BookingItem
{
    public string WorkoutName { get; set; } = string.Empty;
    public string DateText { get; set; } = string.Empty;
}