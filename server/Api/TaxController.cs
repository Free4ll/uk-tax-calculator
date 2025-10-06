using Microsoft.AspNetCore.Mvc;
using Tax.Api.Api.Dto;
using Tax.Api.Domain.Interfaces;
using Tax.Api.Infrastructure.Interfaces;

namespace Tax.Api.Api;

[ApiController]
[Route("api/tax")]
public sealed class TaxController : ControllerBase
{
    private readonly ITaxCalculator _calculator;
    private readonly ITaxBandRepository _repo;

    public TaxController(ITaxCalculator calculator, ITaxBandRepository repo)
    { _calculator = calculator; _repo = repo; }

    [HttpPost("calculate")]
    public async Task<ActionResult<CalculateResponseDto>> Calculate([FromBody] CalculateRequestDto dto)
    {
        var bands = await _repo.GetAllAsync(HttpContext.RequestAborted);
        var result = _calculator.Calculate(dto.GrossAnnualSalary, bands);
        return Ok(new CalculateResponseDto
        {
            GrossAnnualSalary = result.GrossAnnual,
            GrossMonthlySalary = result.GrossMonthly,
            NetAnnualSalary = result.NetAnnual,
            NetMonthlySalary = result.NetMonthly,
            AnnualTaxPaid = result.AnnualTax,
            MonthlyTaxPaid = result.MonthlyTax,
            Slices = result.Slices
        });
    }
}
