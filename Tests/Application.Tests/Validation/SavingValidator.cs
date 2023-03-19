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
    public class SavingValidator : ValidationTestBase
    {
        [Fact]
        public void ShouldSuccess()
        {
            var validator = new Application.DailyActions.DailySavings.SavingValidator(_validationExtensionMock.Object);
            var testRequest = new SavingDto()
            {
                ToAccountName = "testSav",
                FromAccountName = "testCheck",
                Amount = 500.12m,
                GoalName = "test",
                Date = DateTime.UtcNow
            };
            var result = validator.Validate(testRequest);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFail()
        {
            var validator = new Application.DailyActions.DailySavings.SavingValidator(_validationExtensionMock.Object);
            var testRequest = new SavingDto()
            {
                ToAccountName = "testSavFalse",
                FromAccountName = "testCheckFalse",
                GoalName = "testFalse",
                Date = DateTime.UtcNow.AddHours(324)
            };
            var result = validator.Validate(testRequest);

            result.Errors.Count.ShouldBe(8);
            result.IsValid.ShouldBeFalse();
        }
    }
}
