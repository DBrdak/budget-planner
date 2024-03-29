﻿using Application.DTO;
using Application.Tests.Common.TestBase;
using Shouldly;

namespace Application.Tests.MappingProfiles
{
    public class GetMapProfiles : MappingTestBase
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
        [InlineData(nameof(TransactionCategoryDto))]
        public void ShouldMapSuccessfully(string type)
        {
            switch(type)
            {
                case "ExpenditureDto":
                    // Arrange
                    var sourceT = _context.Transactions.FirstOrDefault(x => x.Amount < 0);
                    // Act
                    var destinationE = _mapper.Map<ExpenditureDto>(sourceT);
                    // Assert
                    destinationE.Amount.ShouldBe(sourceT.Amount);
                    destinationE.AccountName.ShouldBe(sourceT.Account.Name);
                    destinationE.Category.ShouldBe(sourceT.Category);
                    destinationE.Date.ShouldBe(sourceT.Date);
                    destinationE.Id.ShouldBe(sourceT.Id);
                    destinationE.Title.ShouldBe(sourceT.Title);
                    break;
                case "IncomeDto":
                    // Arrange
                    sourceT = _context.Transactions.FirstOrDefault(x => x.Amount > 0);
                    // Act
                    var destinationI = _mapper.Map<IncomeDto>(sourceT);
                    // Assert
                    destinationI.Amount.ShouldBe(sourceT.Amount);
                    destinationI.AccountName.ShouldBe(sourceT.Account.Name);
                    destinationI.Category.ShouldBe(sourceT.Category);
                    destinationI.Date.ShouldBe(sourceT.Date);
                    destinationI.Id.ShouldBe(sourceT.Id);
                    destinationI.Title.ShouldBe(sourceT.Title);
                    break;
                case "SavingDto":
                    // Arrange
                    var sourceS = _context.Savings.FirstOrDefault();
                    // Act
                    var destinationS = _mapper.Map<SavingDto>(sourceS);
                    // Assert
                    destinationS.Amount.ShouldBe(sourceS.Amount);
                    destinationS.ToAccountName.ShouldBe(sourceS.ToAccount.Name);
                    destinationS.FromAccountName.ShouldBe(sourceS.FromAccount.Name);
                    destinationS.Date.ShouldBe(sourceS.Date);
                    destinationS.Id.ShouldBe(sourceS.Id);
                    destinationS.GoalName.ShouldBe(sourceS.Goal.Name);
                    break;
                case "FutureExpenditureDto":
                    // Arrange
                    var sourceFT = _context.FutureTransactions.FirstOrDefault(x => x.Amount < 0);
                    // Act
                    var destinationFE = _mapper.Map<FutureExpenditureDto>(sourceFT);
                    // Assert
                    destinationFE.Amount.ShouldBe(sourceFT.Amount);
                    destinationFE.AccountName.ShouldBe(sourceFT.Account.Name);
                    destinationFE.Category.ShouldBe(sourceFT.Category);
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
                    destinationFI.Category.ShouldBe(sourceFT.Category);
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
                case "TransactionCategoryDto":
                    // Arrange
                    var sourceC = _context.TransactionCategories.FirstOrDefault();
                    // Act
                    var destinationC = _mapper.Map<TransactionCategoryDto>(sourceC);
                    // Assert
                    destinationC.Value.ShouldBe(sourceC.Value);
                    destinationC.Id.ShouldBe(sourceC.Id);
                    break;
            }
        }
    }
}
