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
    public class ProfileValidator : ValidationTestBase
    {
        [Fact]
        public void ShouldSuccess()
        {
            var validator = new Application.Profiles.ProfileValidator(_validationProfileExtensionMock.Object);

            var testRequest = new ProfileDto()
            {
                BudgetName = "testBudget",
                Username = "testName",
                Email = "testing@test.com",
                DisplayName = "test"
            };

            var result = validator.Validate(testRequest);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFail()
        {
            var validator = new Application.Profiles.ProfileValidator(_validationProfileExtensionMock.Object);

            var testRequest = new ProfileDto()
            {
                BudgetName = "testBudgetFalseFalseFalseFalseFalseFalseFalseFalseFalseFalse",
                Username = "testNameFalseFalseFalseFalseFalseFalseFalseFalseFalseFalseFalse",
                Email = "test@test.comFalseFalseFalse",
            };

            var result = validator.Validate(testRequest);

            result.Errors.Count.ShouldBe(6);
            result.IsValid.ShouldBeFalse();
        }
    }
}
