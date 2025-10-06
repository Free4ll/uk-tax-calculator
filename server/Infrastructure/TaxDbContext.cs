using Microsoft.EntityFrameworkCore;
using Tax.Api.Domain;

namespace Tax.Api.Infrastructure;

public sealed class TaxDbContext : DbContext
{
    public DbSet<TaxBand> TaxBands => Set<TaxBand>();
    public TaxDbContext(DbContextOptions<TaxDbContext> opts) : base(opts) { }
}
