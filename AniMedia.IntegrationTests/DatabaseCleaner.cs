using System.Text;
using AniMedia.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.IntegrationTests;

public static class DatabaseCleaner {

    public static async Task Clear(this DatabaseContext databaseContext) {
        var tableNames = databaseContext.Model
            .GetEntityTypes()
            .Select(t => t.GetTableName())
            .Distinct()
            .ToList();

        var sqlBuilder = new StringBuilder();

        foreach (var table in tableNames) {
            sqlBuilder.AppendLine($"TRUNCATE TABLE \"{table}\" CASCADE;");
        }

        await databaseContext.Database.ExecuteSqlRawAsync(sqlBuilder.ToString());
    }
}