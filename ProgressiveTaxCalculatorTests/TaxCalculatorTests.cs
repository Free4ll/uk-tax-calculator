using FluentAssertions;
using Tax.Api.Domain;
using Tax.Api.Domain.Interfaces;

namespace ProgressiveTaxCalculatorTests
{
    public class TaxCalculatorTests
    {
        private readonly ITaxCalculator _sut = new ProgressiveTaxCalculator();
        private static readonly List<TaxBand> Bands = new()
    {
        new TaxBand { LowerLimit = 0, UpperLimit = 5000, RatePercent = 0 },
        new TaxBand { LowerLimit = 5000, UpperLimit = 20000, RatePercent = 20 },
        new TaxBand { LowerLimit = 20000, UpperLimit = null, RatePercent = 40 }
    };

        [Theory]
        [InlineData(10000, 1000)]
        [InlineData(40000, 11000)]
        [InlineData(0, 0)]
        public void Calculates_Expected_AnnualTax(decimal gross, decimal expectedTax)
        {
            var result = _sut.Calculate(gross, Bands);
            result.AnnualTax.Should().Be(expectedTax);
        }

        [Fact]
        public void Monthly_Is_Annual_Divided_By_12_Rounded()
        {
            var result = _sut.Calculate(40000, Bands);
            result.GrossMonthly.Should().Be(3333.33m);
            result.MonthlyTax.Should().Be(916.67m);
            result.NetMonthly.Should().Be(2416.67m);
        }
    }
}