using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceDailies.Model;

namespace SpaceDailies;

public partial class MainPageViewModel : ObservableObject
{
	private AstronomyService service;

    [ObservableProperty]
    AstronomyDailyEntry dailyEntry;

    public MainPageViewModel(AstronomyService service)
	{
		this.service = service;
	}

	[RelayCommand]
	async Task GetEntry(string date)
	{
		try
		{
            var entry = await service.FetchDailyEntry(date);
            DailyEntry = entry;
        }
        catch(Exception)
		{
			await displayError();
		}
	}

	private async Task displayError() =>
		await Shell.Current.DisplayAlert("Error", "Unable to download data!", "OK");
	
}

