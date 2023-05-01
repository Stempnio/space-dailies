using System;
using System.Net;
using System.Net.Http.Json;
using SpaceDailies.Model;

namespace SpaceDailies;

public class AstronomyService : IAstronomyService
{
    private HttpClient httpClient;
    private const string baseUrl = "https://api.nasa.gov/";
    private const string apodEndpoint = "planetary/apod";
    private const string demoApiKey = "DEMO_KEY";

    public AstronomyService()
	{
        httpClient = new HttpClient();
	}

    public async Task<AstronomyDailyEntry> FetchDailyEntry(string date)
    {
        var uri = buildUri(date);

        var response = await httpClient.GetAsync(uri);

        if(response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AstronomyDailyEntry>();
        } else
        {
            throw new Exception($"AstronomyService exception, {response.StatusCode}");
        }
    }

    private string buildUri(string date)
    {
        UriBuilder uriBuilder = new UriBuilder(baseUrl);
        uriBuilder.Path += apodEndpoint;
        uriBuilder.Query = $"api_key={demoApiKey}&date={Uri.EscapeDataString(date)}";

        return uriBuilder.Uri.ToString();
    }
}

