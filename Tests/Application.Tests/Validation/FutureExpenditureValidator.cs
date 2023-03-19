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
    public class FutureExpenditureValidator : ValidationTestBase
    {
        [Fact]
        public void ShouldSuccess()
        {
            var validator = new Application.SpendingPlan.Expenditures.FutureExpenditureValidator(_validationExtensionMock.Object);
            var testRequest = new FutureExpenditureDto()
            {
                AccountName = "testCheck",
                Amount = 500.12m,
                Category = "test",
                Date = DateTime.UtcNow.AddDays(11)
            };
            var result = validator.Validate(testRequest);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFail()
        {
            var validator = new Application.SpendingPlan.Expenditures.FutureExpenditureValidator(_validationExtensionMock.Object);
            var testRequest = new FutureExpenditureDto()
            {
                AccountName = "testCheckFalse",
                Category = "testFalseFalseFalseFalseFalseFalseFalseFalseFalseFalseFalseFalseFalseFalseFalseFalse",
                Date = DateTime.UtcNow.AddDays(-11)
            };
            var result = validator.Validate(testRequest);

            result.Errors.Count.ShouldBe(5);
            result.IsValid.ShouldBeFalse();
        }
    }
}
