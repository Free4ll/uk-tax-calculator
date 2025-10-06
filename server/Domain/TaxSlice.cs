namespace Tax.Api.Domain
{
    public sealed record TaxSlice(string BandName, decimal TaxableAmount, decimal RatePercent, decimal TaxPaid);
}
