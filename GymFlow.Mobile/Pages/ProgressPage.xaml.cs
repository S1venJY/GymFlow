using GymFlow.Mobile.Models;
using GymFlow.Mobile.Services;

namespace GymFlow.Mobile.Pages;

public partial class ProgressPage : ContentPage
{
    private readonly ApiService _apiService;

    public ProgressPage()
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

        ProgressContent.IsVisible = false;

        try
        {
            var progress =
                await _apiService.GetProgressAsync();

            if (progress == null)
            {
                progress =
                    _apiService.GetDemoProgress();
            }

            ShowProgress(progress);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);

            ShowProgress(
                _apiService.GetDemoProgress());
        }
    }

    private void ShowProgress(
        ProgressDto progress)
    {
        WorkoutsCountLabel.Text =
            progress.WorkoutsCountText;

        TotalVolumeLabel.Text =
            progress.TotalVolumeText;

        MaxWeightLabel.Text =
            progress.MaxWeightText;

        StreakLabel.Text =
            progress.CurrentStreakText;

        LoadingBlock.IsVisible = false;
        LoadingIndicator.IsRunning = false;

        ProgressContent.IsVisible = true;
    }
}