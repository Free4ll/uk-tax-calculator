using FluentValidation;
using Tax.Api.Api.Dto;

namespace Tax.Api.Api;

public sealed class CalculateRequestValidator : AbstractValidator<CalculateRequestDto>
{
    public CalculateRequestValidator()
    {
        RuleFor(x => x.GrossAnnualSalary)
            .NotNull().WithMessage("Gross Annual Salary is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Salary must be >= 0.");
    }
}
