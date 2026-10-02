using GymFlow.Mobile.Models;
using GymFlow.Mobile.Services;

namespace GymFlow.Mobile.Pages;

public partial class HomePage : ContentPage
{
    private readonly ApiService _apiService;

    public HomePage()
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

            if (classes == null)
            {
                classes = _apiService.GetDemoClasses();
            }

            if (classes.Count == 0)
            {
                SetState(LoadingState.Empty);
                return;
            }

            ClassesCollectionView.ItemsSource = classes;

            SetState(LoadingState.Success);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);

            ClassesCollectionView.ItemsSource =
                _apiService.GetDemoClasses();

            SetState(LoadingState.Success);
        }
    }

    private void SetState(LoadingState state)
    {
        LoadingBlock.IsVisible =
            state == LoadingState.Loading;

        LoadingIndicator.IsRunning =
            state == LoadingState.Loading;

        ErrorBlock.IsVisible =
            state == LoadingState.Error;

        EmptyBlock.IsVisible =
            state == LoadingState.Empty;

        ClassesCollectionView.IsVisible =
            state == LoadingState.Success;
    }

    private async void OnBookClicked(
        object sender,
        EventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.CommandParameter is not int classId)
            return;

        button.IsEnabled = false;
        button.Text = "Запис...";

        try
        {
            bool success =
                await _apiService.BookClassAsync(classId);

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
                    "Запис виконано у тестовому режимі.",
                    "OK");
            }

            button.Text = "Записано";
            button.BackgroundColor = Colors.Gray;
        }
        catch
        {
            await DisplayAlert(
                "GymFlow",
                "Запис виконано у тестовому режимі.",
                "OK");

            button.Text = "Записано";
            button.BackgroundColor = Colors.Gray;
        }
    }

    private async void OnRetryClicked(
        object sender,
        EventArgs e)
    {
        await LoadDataAsync();
    }

    private enum LoadingState
    {
        Loading,
        Success,
        Error,
        Empty
    }
}