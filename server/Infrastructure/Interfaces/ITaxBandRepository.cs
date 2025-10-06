using Tax.Api.Domain;

namespace Tax.Api.Infrastructure.Interfaces
{
    public interface ITaxBandRepository
    {
        Task<IReadOnlyList<TaxBand>> GetAllAsync(CancellationToken ct = default);
    }
}
