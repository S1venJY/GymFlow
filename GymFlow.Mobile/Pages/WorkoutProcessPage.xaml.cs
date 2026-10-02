using Microsoft.Maui.Controls;
using System;

namespace GymFlow.Mobile.Pages;

public partial class WorkoutProcessPage : ContentPage
{
    private int _secondsLeft = 15;
    private bool _isTimerRunning = false;

    public WorkoutProcessPage()
    {
        InitializeComponent();
    }

    private void OnStartWorkoutClicked(object sender, EventArgs e)
    {
        if (_isTimerRunning) return;

        _isTimerRunning = true;
        StartButton.IsEnabled = false;

        Application.Current?.Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            _secondsLeft--;
            TimerLabel.Text = _secondsLeft.ToString();

            if (_secondsLeft <= 0)
            {
                _isTimerRunning = false;
                DisplayAlert("Чудово!", "Вправу успішно виконано!", "OK");
                StartButton.IsEnabled = true;
                _secondsLeft = 15;
                TimerLabel.Text = "15";
                return false;
            }

            return true;
        });
    }
}