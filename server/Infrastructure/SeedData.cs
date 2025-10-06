using Microsoft.EntityFrameworkCore;
using Tax.Api.Domain;

namespace Tax.Api.Infrastructure;

public static class SeedData
{
    public static async Task EnsureSeededAsync(this TaxDbContext db)
    {
        if (!await db.TaxBands.AnyAsync())
        {
            db.TaxBands.AddRange(
                new TaxBand { LowerLimit = 0, UpperLimit = 5000, RatePercent = 0 },
                new TaxBand { LowerLimit = 5000, UpperLimit = 20000, RatePercent = 20 },
                new TaxBand { LowerLimit = 20000, UpperLimit = null, RatePercent = 40 }
            );
            await db.SaveChangesAsync();
        }
    }
}
