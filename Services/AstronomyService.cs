using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using SpaceDailies.Model;

namespace SpaceDailies;

public class AstronomyService : IAstronomyService
{
    private IAstronomyStorage storage;
    private HttpClient httpClient;
    private const string baseUrl = "https://api.nasa.gov/";
    private const string apodEndpoint = "planetary/apod";

    public AstronomyService(IAstronomyStorage storage)
    {
        httpClient = new HttpClient();
        this.storage = storage;
    }

    public async Task<AstronomyDailyEntry> FetchDailyEntry(string date)
    {
        var cachedEntry = await storage.GetEntry(date);
        if (cachedEntry is not null)
        {
            return cachedEntry;
        }


        var uri = buildUri(date);

        var response = await httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var entry = await response.Content.ReadFromJsonAsync<AstronomyDailyEntry>();
            _ = storage.SaveEntry(entry);
            return entry;
        }
        else
        {
            throw new Exception($"AstronomyService exception, {response.StatusCode}");
        }
    }

    private string buildUri(string date)
    {
        UriBuilder uriBuilder = new UriBuilder(baseUrl);
        uriBuilder.Path += apodEndpoint;
        uriBuilder.Query = $"api_key={Secrets.apiKey}&date={Uri.EscapeDataString(date)}";

        return uriBuilder.Uri.ToString();
    }
}
