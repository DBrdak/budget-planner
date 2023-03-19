using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Tests.Common.TestBase;
using Shouldly;

namespace Application.Tests.Validation
{
    public class AccountValidator : ValidationTestBase
    {
        [Fact]
        public void ShouldSuccess()
        {
            var validator = new Accounts.AccountValidator(_validationExtensionMock.Object);
            var testRequest = new AccountDto
            {
                AccountType = "Checking",
                Balance = 255.12m,
                Name = "test"
            };
            var result = validator.Validate(testRequest);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFail()
        {
            var validator = new Accounts.AccountValidator(_validationExtensionMock.Object);
            var testRequest = new AccountDto
            {
                AccountType = "Checkings",
                Name = "test sadasdasda sdasdas dsadsa"
            };
            var result = validator.Validate(testRequest);

            result.Errors.Count.ShouldBe(4);
            result.IsValid.ShouldBeFalse();
        }
    }
}
