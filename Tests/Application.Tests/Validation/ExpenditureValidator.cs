using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Tests.Common.TestBase;
using Application.DailyActions.DailyExpenditures;
using Shouldly;

namespace Application.Tests.Validation
{
    public class ExpenditureValidator : ValidationTestBase
    {
        [Fact]
        public void ShouldSuccess()
        {
            var validator = new Application.DailyActions.DailyExpenditures.ExpenditureValidator(_validationExtensionMock.Object);
            var testRequest = new ExpenditureDto()
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
            var validator = new Application.DailyActions.DailyExpenditures.ExpenditureValidator(_validationExtensionMock.Object);
            var testRequest = new ExpenditureDto()
            {
                Title = "testingFalseFalseFalseFalseFalseFalseFalseFalse",
                AccountName = "testFalse",
                Category = "testFalse",
                Date = DateTime.UtcNow.AddDays(5)
            };
            var result = validator.Validate(testRequest);

            result.Errors.Count.ShouldBe(6);
            result.IsValid.ShouldBeFalse();
        }
    }
}
