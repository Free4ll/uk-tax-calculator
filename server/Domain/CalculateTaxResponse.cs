namespace Tax.Api.Domain;

public sealed record CalculateTaxResponse(
    decimal GrossAnnual,
    decimal GrossMonthly,
    decimal NetAnnual,
    decimal NetMonthly,
    decimal AnnualTax,
    decimal MonthlyTax,
    IReadOnlyList<TaxSlice> Slices);
