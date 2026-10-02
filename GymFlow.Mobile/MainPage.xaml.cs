using GymFlow.Mobile.Models;
using GymFlow.Mobile.Services;

namespace GymFlow.Mobile;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;

    public MainPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        SetState(LoadingState.Loading);

        try
        {
            var classes = await _apiService.GetGymClassesAsync();

            if (classes != null && classes.Any())
            {
                ClassesCollectionView.ItemsSource = classes;
            }
            else
            {
                ClassesCollectionView.ItemsSource = GetFallbackData();
            }

            SetState(LoadingState.Success);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Помилка завантаження: {ex.Message}");

            ClassesCollectionView.ItemsSource = GetFallbackData();

            SetState(LoadingState.Success);
        }
    }

    private void SetState(LoadingState state)
    {
        switch (state)
        {
            case LoadingState.Loading:

                LoadingBlock.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                ErrorBlock.IsVisible = false;
                ClassesCollectionView.IsVisible = false;

                break;

            case LoadingState.Success:

                LoadingBlock.IsVisible = false;
                LoadingIndicator.IsRunning = false;

                ErrorBlock.IsVisible = false;
                ClassesCollectionView.IsVisible = true;

                break;

            case LoadingState.Error:

                LoadingBlock.IsVisible = false;
                LoadingIndicator.IsRunning = false;

                ErrorBlock.IsVisible = true;
                ClassesCollectionView.IsVisible = false;

                break;
        }
    }

    private async void OnBookClicked(object sender, EventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.CommandParameter is not int classId)
            return;

        button.IsEnabled = false;
        button.Text = "Запис...";

        try
        {
            bool success = await _apiService.BookClassAsync(classId);

            if (success)
            {
                await DisplayAlert(
                    "Успіх",
                    "Ви успішно записалися на тренування!",
                    "OK");
            }
            else
            {
                await DisplayAlert(
                    "GymFlow",
                    "Заявку прийнято! (Тестовий режим)",
                    "OK");
            }

            button.Text = "Записано";
            button.BackgroundColor = Colors.Gray;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Помилка запису: {ex.Message}");

            await DisplayAlert(
                "GymFlow",
                "Заявку прийнято! (Тестовий режим)",
                "OK");

            button.Text = "Записано";
            button.BackgroundColor = Colors.DarkSlateGray;
        }
    }

    private async void OnRetryClicked(object sender, EventArgs e)
    {
        await LoadDataAsync();
    }

    private List<GymClassDto> GetFallbackData()
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
                2)
        };
    }

    private enum LoadingState
    {
        Loading,
        Success,
        Error
    }
}