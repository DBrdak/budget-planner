using Application.DailyActions.DailyIncomes;
using Application.DTO;
using Application.Tests.Common;
using Shouldly;

namespace Application.Tests.DailyActions.Comand.Income;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var income = new IncomeDto
        {
            AccountName = "CheckingTest",
            Amount = 123,
            Category = "TestingCategory",
            Date = DateTime.UtcNow.AddDays(21)
        };

        // Act
        var result = await handler.Handle(new Create.Command { NewIncome = income }, CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}