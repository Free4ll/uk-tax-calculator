namespace Tax.Api.Domain.Interfaces;

public interface ITaxCalculator
{
    CalculateTaxResponse Calculate(decimal grossAnnualSalary, IReadOnlyList<TaxBand> bands);
}
