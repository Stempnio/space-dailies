using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceDailies.Model;

namespace SpaceDailies;

public partial class MainPageViewModel : BaseViewModel
{
    private AstronomyService service;

    [ObservableProperty]
    AstronomyDailyEntry dailyEntry;

    public MainPageViewModel(AstronomyService service)
    {
        this.service = service;
    }

    public override void init()
    {
        var today = formatDate(DateTime.Today);
        Task.Run(async () => await GetEntry(today));
    }

    [RelayCommand]
    async Task GetPreviousEntry()
    {
        var currentDate = DailyEntry.date;
        var nextDate = addDaysToDate(currentDate, -1);

        await GetEntry(nextDate);
    }

    [RelayCommand]
    async Task GetNextEntry()
    {
        var currentDate = DailyEntry.date;

        if (isToday(currentDate))
        {
            await displayMaxIndexError();
            return;
        }

        var nextDate = addDaysToDate(currentDate, 1);

        await GetEntry(nextDate);

    }

    private bool isToday(string date)
    {
        var parsedDate = DateTime.Parse(date);
        return parsedDate == DateTime.Today;
    }

    private string addDaysToDate(string date, int amount)
    {
        var parsedDate = DateTime.Parse(date);
        var resultDate = parsedDate.AddDays(amount);

        return formatDate(resultDate);
    }

    private string formatDate(DateTime date) => date.ToString("yyyy-MM-dd");

    async Task GetEntry(string date)
    {
        try
        {
            var entry = await service.FetchDailyEntry(date);
            DailyEntry = entry;
        }
        catch (Exception)
        {
            await displayNetworkError();
        }
    }

    private async Task displayNetworkError() =>
        await Shell.Current.DisplayAlert("Error", "Unable to download data!", "OK");

    private async Task displayMaxIndexError() =>
        await Shell.Current.DisplayAlert("You've reached the end!", "You can't fetch tommorows data. It doesn't exist yet!", "OK");
}
