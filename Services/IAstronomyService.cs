using System;
using SpaceDailies.Model;

namespace SpaceDailies;

public interface IAstronomyService
{
	public Task<AstronomyDailyEntry> FetchDailyEntry(string date);
}

