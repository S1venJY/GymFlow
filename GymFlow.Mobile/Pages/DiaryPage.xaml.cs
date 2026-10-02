using GymFlow.Mobile.Models;
using GymFlow.Mobile.Services;

namespace GymFlow.Mobile.Pages;

public partial class DiaryPage : ContentPage
{
    private readonly ApiService _apiService;

    public DiaryPage()
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
        LoadingBlock.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        WorkoutCollectionView.IsVisible = false;
        EmptyBlock.IsVisible = false;

        try
        {
            var workouts =
                await _apiService.GetWorkoutsAsync();

            if (workouts == null)
            {
                workouts =
                    _apiService.GetDemoWorkouts();
            }

            LoadingBlock.IsVisible = false;
            LoadingIndicator.IsRunning = false;

            if (workouts.Count == 0)
            {
                EmptyBlock.IsVisible = true;
                return;
            }

            WorkoutCollectionView.ItemsSource = workouts;
            WorkoutCollectionView.IsVisible = true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);

            LoadingBlock.IsVisible = false;
            LoadingIndicator.IsRunning = false;

            WorkoutCollectionView.ItemsSource =
                _apiService.GetDemoWorkouts();

            WorkoutCollectionView.IsVisible = true;
        }
    }

    private async void OnAddWorkoutClicked(
        object sender,
        EventArgs e)
    {
        string exercise =
            await DisplayPromptAsync(
                "Нова вправа",
                "Назва вправи:");

        if (string.IsNullOrWhiteSpace(exercise))
            return;

        string weightText =
            await DisplayPromptAsync(
                "Робоча вага",
                "Вага, кг:");

        if (!double.TryParse(weightText, out double weight))
        {
            await DisplayAlert(
                "Помилка",
                "Введіть коректну вагу.",
                "OK");

            return;
        }

        string repsText =
            await DisplayPromptAsync(
                "Повторення",
                "Кількість повторень:");

        if (!int.TryParse(repsText, out int repetitions))
        {
            await DisplayAlert(
                "Помилка",
                "Введіть коректну кількість повторень.",
                "OK");

            return;
        }

        string setsText =
            await DisplayPromptAsync(
                "Підходи",
                "Кількість підходів:");

        if (!int.TryParse(setsText, out int sets))
        {
            await DisplayAlert(
                "Помилка",
                "Введіть коректну кількість підходів.",
                "OK");

            return;
        }

        var workout = new WorkoutDto(
            Random.Shared.Next(1000, 9999),
            exercise,
            "Інша",
            weight,
            repetitions,
            sets,
            DateTime.Now);

        bool saved =
            await _apiService.AddWorkoutAsync(workout);

        if (!saved)
        {
            await DisplayAlert(
                "GymFlow",
                "Результат додано у тестовому режимі.",
                "OK");
        }
        else
        {
            await DisplayAlert(
                "Успіх",
                "Результат тренування збережено.",
                "OK");
        }

        await LoadDataAsync();
    }
}