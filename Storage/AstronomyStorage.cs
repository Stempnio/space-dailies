using System;
using SpaceDailies.Model;
using SQLite;

namespace SpaceDailies.Storage;

public class AstronomyStorage : IAstronomyStorage
{

    SQLiteAsyncConnection Database;

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<AstronomyDailyEntry>();
    }

    public AstronomyStorage()
    {
    }

    public async Task<AstronomyDailyEntry> GetEntry(string date)
    {
        await Init();
        return await Database.Table<AstronomyDailyEntry>()
            .Where(i => i.date == date)
            .FirstOrDefaultAsync();
    }

    public async Task SaveEntry(AstronomyDailyEntry entry)
    {
        await Init();
        await Database.InsertAsync(entry);
    }
}

