using Tax.Api.Domain;

namespace Tax.Api.Api.Dto
{
    public sealed class CalculateResponseDto
    {
        public decimal GrossAnnualSalary { get; set; }
        public decimal GrossMonthlySalary { get; set; }
        public decimal NetAnnualSalary { get; set; }
        public decimal NetMonthlySalary { get; set; }
        public decimal AnnualTaxPaid { get; set; }
        public decimal MonthlyTaxPaid { get; set; }
        public IReadOnlyList<TaxSlice> Slices { get; set; } = Array.Empty<TaxSlice>();
    }
}
