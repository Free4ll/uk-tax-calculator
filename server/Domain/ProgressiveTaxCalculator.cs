using Tax.Api.Domain.Interfaces;

namespace Tax.Api.Domain;

public sealed class ProgressiveTaxCalculator : ITaxCalculator
{
    private const decimal MonthsInYear = 12m;
    private const int DecimalPlaces = 2;
    private const decimal ZeroAmount = 0m;
    private const char FirstBandLetter = 'A';
    private const string BandPrefix = "Band";

    public CalculateTaxResponse Calculate(decimal grossAnnual, IReadOnlyList<TaxBand> bands)
    {
        if (grossAnnual < 0m) throw new ArgumentOutOfRangeException(nameof(grossAnnual));
        if (bands is null || bands.Count == 0) throw new ArgumentException("No tax bands configured");

        var ordered = bands.OrderBy(b => b.LowerLimit).ToList();

        decimal annualTax = 0m;
        var slices = new List<TaxSlice>();

        for (int i = 0; i < ordered.Count; i++)
        {
            var band = ordered[i];
            var nextLower = (i < ordered.Count - 1) ? ordered[i + 1].LowerLimit : (int?)null;
            var upper = nextLower ?? band.UpperLimit;

            var lower = (decimal)band.LowerLimit;
            var upperBound = upper.HasValue ? (decimal)upper.Value : decimal.MaxValue;

            var taxable = Math.Max(ZeroAmount, Math.Min(grossAnnual, upperBound) - lower);
            if (taxable <= ZeroAmount) continue;

            var rate = band.RatePercent / 100m;
            var tax = Math.Round(taxable * rate, DecimalPlaces, MidpointRounding.AwayFromZero);

            annualTax += tax;
            slices.Add(new TaxSlice(
                BandName: $"{BandPrefix} {(char)(FirstBandLetter + i)}",
                TaxableAmount: Math.Round(taxable, DecimalPlaces),
                RatePercent: band.RatePercent,
                TaxPaid: tax));
        }

        var netAnnual = Math.Round(grossAnnual - annualTax, DecimalPlaces);
        return new CalculateTaxResponse(
            GrossAnnual: Math.Round(grossAnnual, DecimalPlaces),
            GrossMonthly: Math.Round(grossAnnual / MonthsInYear, DecimalPlaces),
            NetAnnual: netAnnual,
            NetMonthly: Math.Round(netAnnual / MonthsInYear, DecimalPlaces),
            AnnualTax: annualTax,
            MonthlyTax: Math.Round(annualTax / MonthsInYear, DecimalPlaces),
            Slices: slices);
    }
}
