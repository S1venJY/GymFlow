using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;

namespace GymFlow.Mobile.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Помилка", "Заповніть всі поля", "OK");
            return;
        }

        Preferences.Set("UserEmail", EmailEntry.Text);
        Preferences.Set("IsLoggedIn", true);

        await DisplayAlert("Успіх", "Ви успішно увійшли!", "OK");
        await Navigation.PopAsync();
    }
}