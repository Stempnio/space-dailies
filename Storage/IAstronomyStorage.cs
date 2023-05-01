using System;
using SpaceDailies.Model;

namespace SpaceDailies;

public interface IAstronomyStorage
{
    public Task<AstronomyDailyEntry> GetEntry(string date);

    public Task SaveEntry(AstronomyDailyEntry entry);
}

