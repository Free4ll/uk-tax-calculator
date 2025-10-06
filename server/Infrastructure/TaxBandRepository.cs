using Microsoft.EntityFrameworkCore;
using Tax.Api.Domain;
using Tax.Api.Infrastructure.Interfaces;

namespace Tax.Api.Infrastructure;

public sealed class TaxBandRepository : ITaxBandRepository
{
    private readonly TaxDbContext _db;
    public TaxBandRepository(TaxDbContext db) => _db = db;

    public async Task<IReadOnlyList<TaxBand>> GetAllAsync(CancellationToken ct = default)
        => await _db.TaxBands.AsNoTracking().OrderBy(b => b.LowerLimit).ToListAsync(ct);
}
