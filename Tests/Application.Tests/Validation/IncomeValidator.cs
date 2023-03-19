using Application.DTO;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Tests.Common.TestBase;

namespace Application.Tests.Validation
{
    public class IncomeValidator : ValidationTestBase
    {
        [Fact]
        public void ShouldSuccess()
        {
            var validator = new Application.DailyActions.DailyIncomes.IncomeValidator(_validationExtensionMock.Object);
            var testRequest = new IncomeDto()
            {
                Title = "testing",
                AccountName = "test",
                Amount = 500.12m,
                Category = "test",
                Date = DateTime.UtcNow
            };
            var result = validator.Validate(testRequest);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFail()
        {
            var validator = new Application.DailyActions.DailyIncomes.IncomeValidator(_validationExtensionMock.Object);
            var testRequest = new IncomeDto()
            {
                Title = "testingFalseFalseFalseFalseFalseFalseFalseFalseFalseFalse",
                AccountName = "testFalse",
                Category = "testFalse",
                Date = DateTime.UtcNow.AddDays(1)
            };
            var result = validator.Validate(testRequest);

            result.Errors.Count.ShouldBe(6);
            result.IsValid.ShouldBeFalse();
        }
    }
}
