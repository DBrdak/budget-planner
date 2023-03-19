using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Tests.Common.DataContextBase;
using Moq;
using Persistence;

namespace Application.Tests.Common.TestBase
{
    public class ValidationTestBase
    {
        protected readonly Mock<IValidationExtension> _validationExtensionMock;
        protected readonly Mock<IProfileValidationExtension> _validationProfileExtensionMock;
        private readonly DataContext _context;

        public ValidationTestBase()
        {
            _context = DataContextFactory.Create();
            _validationExtensionMock = new Mock<IValidationExtension>();
            _validationProfileExtensionMock = new Mock<IProfileValidationExtension>();
            SetupValidationExtensionMock();
        }

        private void SetupValidationExtensionMock()
        {
            _validationExtensionMock.Setup(x => x.UniqueAccountName("test")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.AccountExists("test")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.AccountExists("testSav")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.AccountExists("testCheck")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.CategoryExists("test", "expenditure")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.CategoryExists("test", "income")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.AccountTypeOf("testSav", "Saving")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.AccountTypeOf("testCheck", "Checking")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.GoalExists("test")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.UniqueCategory("test", "expenditure")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.UniqueCategory("test", "income")).ReturnsAsync(true);
            _validationExtensionMock.Setup(x => x.UniqueGoalName("test")).ReturnsAsync(true);

            _validationProfileExtensionMock.Setup(x => x.UniqueEmail("testing@test.com")).ReturnsAsync(true);
            _validationProfileExtensionMock.Setup(x => x.UniqueBudgetName("testBudget")).ReturnsAsync(true);
            _validationProfileExtensionMock.Setup(x => x.UniqueUsername("testName")).ReturnsAsync(true);
        }
    }
}
