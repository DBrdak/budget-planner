﻿using Application.DTO;
using Application.Tests.Common.TestBase;
using Domain;
using Shouldly;

namespace Application.Tests.MappingProfiles
{
    public class SetMapProfiles : MappingTestBase
    {
        [Theory]
        [InlineData(nameof(ExpenditureDto))]
        [InlineData(nameof(IncomeDto))]
        [InlineData(nameof(SavingDto))]
        [InlineData(nameof(FutureExpenditureDto))]
        [InlineData(nameof(FutureIncomeDto))]
        [InlineData(nameof(FutureSavingDto))]
        [InlineData(nameof(GoalDto))]
        [InlineData(nameof(AccountDto))]
        [InlineData(nameof(ProfileDto))]
        public void ShouldMapSuccessfully(string type)
        {
            switch (type)
            {
                case "ExpenditureDto":
                    // Arrange
                    var sourceE = new ExpenditureDto();
                    // Act
                    var destinationT = _mapper.Map<Transaction>(sourceE);
                    // Assert
                    destinationT.Amount.ShouldBe(sourceE.Amount);
                    destinationT.Date.ShouldBe(sourceE.Date);
                    destinationT.Id.ShouldBe(sourceE.Id);
                    destinationT.Title.ShouldBe(sourceE.Title);
                    break;
                case "IncomeDto":
                    // Arrange
                    var sourceTi = new IncomeDto();
                    // Act
                    var destinationI = _mapper.Map<IncomeDto>(sourceTi);
                    // Assert
                    destinationI.Amount.ShouldBe(sourceTi.Amount);
                    destinationI.Date.ShouldBe(sourceTi.Date);
                    destinationI.Id.ShouldBe(sourceTi.Id);
                    destinationI.Title.ShouldBe(sourceTi.Title);
                    break;
                case "SavingDto":
                    // Arrange
                    var sourceS = new SavingDto();
                    // Act
                    var destinationS = _mapper.Map<SavingDto>(sourceS);
                    // Assert
                    destinationS.Amount.ShouldBe(sourceS.Amount);
                    destinationS.ToAccountName.ShouldBe(sourceS.ToAccountName);
                    destinationS.FromAccountName.ShouldBe(sourceS.FromAccountName);
                    destinationS.Date.ShouldBe(sourceS.Date);
                    destinationS.Id.ShouldBe(sourceS.Id);
                    destinationS.GoalName.ShouldBe(sourceS.GoalName);
                    break;
                case "FutureExpenditureDto":
                    // Arrange
                    var sourceFT = _context.FutureTransactions.FirstOrDefault(x => x.Amount < 0);
                    // Act
                    var destinationFE = _mapper.Map<FutureExpenditureDto>(sourceFT);
                    // Assert
                    destinationFE.Amount.ShouldBe(sourceFT.Amount);
                    destinationFE.AccountName.ShouldBe(sourceFT.Account.Name);
                    destinationFE.Date.ShouldBe(sourceFT.Date);
                    destinationFE.Id.ShouldBe(sourceFT.Id);
                    destinationFE.CompletedAmount.ShouldBe(sourceFT.CompletedAmount);
                    destinationFE.CompletedExpenditures.Count()
                        .ShouldBe(sourceFT.CompletedTransactions.Count());
                    break;
                case "FutureIncomeDto":
                    // Arrange
                    sourceFT = _context.FutureTransactions.FirstOrDefault(x => x.Amount > 0);
                    // Act
                    var destinationFI = _mapper.Map<FutureIncomeDto>(sourceFT);
                    // Assert
                    destinationFI.Amount.ShouldBe(sourceFT.Amount);
                    destinationFI.AccountName.ShouldBe(sourceFT.Account.Name);
                    destinationFI.Date.ShouldBe(sourceFT.Date);
                    destinationFI.Id.ShouldBe(sourceFT.Id);
                    destinationFI.CompletedAmount.ShouldBe(sourceFT.CompletedAmount);
                    destinationFI.CompletedIncomes.Count()
                        .ShouldBe(sourceFT.CompletedTransactions.Count());
                    break;
                case "FutureSavingDto":
                    // Arrange
                    var sourceFS = _context.FutureSavings.FirstOrDefault();
                    // Act
                    var destinationFS = _mapper.Map<FutureSavingDto>(sourceFS);
                    // Assert
                    destinationFS.Amount.ShouldBe(sourceFS.Amount);
                    destinationFS.ToAccountName.ShouldBe(sourceFS.ToAccount.Name);
                    destinationFS.FromAccountName.ShouldBe(sourceFS.FromAccount.Name);
                    destinationFS.Date.ShouldBe(sourceFS.Date);
                    destinationFS.Id.ShouldBe(sourceFS.Id);
                    destinationFS.GoalName.ShouldBe(sourceFS.Goal.Name);
                    destinationFS.CompletedSavings.Count()
                        .ShouldBe(sourceFS.CompletedSavings.Count());
                    break;
                case "GoalDto":
                    // Arrange
                    var sourceG = _context.Goals.FirstOrDefault();
                    // Act
                    var destinationG = _mapper.Map<GoalDto>(sourceG);
                    // Assert
                    destinationG.Name.ShouldBe(sourceG.Name);
                    destinationG.Description.ShouldBe(sourceG.Description);
                    destinationG.Id.ShouldBe(sourceG.Id);
                    destinationG.EndDate.ShouldBe(sourceG.EndDate);
                    destinationG.RequiredAmount.ShouldBe(sourceG.RequiredAmount);
                    destinationG.CurrentAmount.ShouldBe(sourceG.CurrentAmount);
                    break;
                case "AccountDto":
                    // Arrange
                    var sourceA = _context.Accounts.FirstOrDefault();
                    // Act
                    var destinationA = _mapper.Map<AccountDto>(sourceA);
                    // Assert
                    destinationA.Balance.ShouldBe(sourceA.Balance);
                    destinationA.AccountType.ShouldBe(sourceA.AccountType);
                    destinationA.Id.ShouldBe(sourceA.Id);
                    destinationA.Name.ShouldBe(sourceA.Name);
                    destinationA.SavingsIn.Count().ShouldBe
                        (sourceA.SavingsIn.Count());
                    destinationA.SavingsOut.Count().ShouldBe
                        (sourceA.SavingsOut.Count());
                    destinationA.Expenditures.Count().ShouldBe
                        (sourceA.Transactions.Where(x => x.Amount < 0).Count());
                    destinationA.Incomes.Count().ShouldBe
                        (sourceA.Transactions.Where(x => x.Amount > 0).Count());
                    break;
                case "ProfileDto":
                    // Arrange
                    var sourceU = _context.Users.FirstOrDefault();
                    // Act
                    var destinationP = _mapper.Map<ProfileDto>(sourceU, opt =>
                        opt.Items["BudgetName"] = _context.Budgets.FirstOrDefault(x => x.UserId == sourceU.Id)?.Name);
                    // Assert
                    destinationP.BudgetName.ShouldBe
                        (_context.Budgets.FirstOrDefault(x => x.UserId == sourceU.Id)?.Name);
                    destinationP.Email.ShouldBe(sourceU?.Email);
                    destinationP.DisplayName.ShouldBe(sourceU?.DisplayName);
                    destinationP.Username.ShouldBe(sourceU?.UserName);
                    break;
            }
        }
    }
}
