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
    public class GoalValidator : ValidationTestBase
    {
        [Fact]
        public void ShouldSuccess()
        {
            var validator = new Application.Goals.GoalValidator(_validationExtensionMock.Object);
            var testRequest = new GoalDto
            {
                Name = "test",
                CurrentAmount = 505,
                EndDate = DateTime.UtcNow.AddDays(501),
                RequiredAmount = 505050
            };
            var result = validator.Validate(testRequest);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFail()
        {
            var validator = new Application.Goals.GoalValidator(_validationExtensionMock.Object);
            var testRequest = new GoalDto
            {
                Name = "testFalseFalseFalseFalseFalseFalseFalseFalseFalseFalse",
                CurrentAmount = 505,
                EndDate = DateTime.UtcNow.AddDays(-501),
                RequiredAmount = 50
            };
            var result = validator.Validate(testRequest);

            result.Errors.Count.ShouldBe(3);
            result.IsValid.ShouldBeFalse();
        }
    }
}
