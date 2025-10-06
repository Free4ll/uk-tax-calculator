namespace Tax.Api.Domain;

public sealed class TaxBand
{
    public int Id { get; set; }
    public int LowerLimit { get; set; } 
    public int? UpperLimit { get; set; } 
    public int RatePercent { get; set; }
}
