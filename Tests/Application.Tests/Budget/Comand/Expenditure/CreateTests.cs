using Application.DailyActions.DailyExpenditures;
using Application.DTO;
using Application.Tests.Common;
using Shouldly;

namespace Application.Tests.DailyActions.Comand.Expenditure;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var expenditure = new ExpenditureDto
        {
            AccountName = "CheckingTest",
            Amount = 123,
            Category = "TestingCategory",
            Date = DateTime.UtcNow.AddDays(21)
        };

        // Act
        var result = await handler.Handle(new Create.Command { NewExpenditure = expenditure}, CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}