using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Tests.Common.TestBase;
using Shouldly;

namespace Application.Tests.Validation
{
    public class FutureSavingValidator : ValidationTestBase
    {
        [Fact]
        public void ShouldSuccess()
        {
            var validator = new Application.SpendingPlan.Savings.FutureSavingValidator(_validationExtensionMock.Object);
            var testRequest = new FutureSavingDto()
            {
                FromAccountName = "testCheck",
                ToAccountName = "testSav",
                Amount = 500.12m,
                GoalName = "test",
                Date = DateTime.UtcNow.AddDays(11)
            };
            var result = validator.Validate(testRequest);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFail()
        {
            var validator = new Application.SpendingPlan.Savings.FutureSavingValidator(_validationExtensionMock.Object);
            var testRequest = new FutureSavingDto()
            {
                FromAccountName = "testCheckFalse",
                ToAccountName = "testSavFalse",
                GoalName = "testFalse",
                Date = DateTime.UtcNow.AddDays(-11)
            };
            var result = validator.Validate(testRequest);

            result.Errors.Count.ShouldBe(5);
            result.IsValid.ShouldBeFalse();
        }
    }
}
