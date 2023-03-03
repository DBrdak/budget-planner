using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IValidationExtension
    {
        public Task<bool> UniqueUsername(string newUsername);

        public Task<bool> UniqueEmail(string newEmail);

        public Task<bool> UniqueBudgetName(string newBudgetName);

        public Task<bool> UniqueCategory(string categoryName, string categoryType);

        public Task<bool> AccountExists(string accountName);

        public Task<bool> AccountTypeOf(string accountName, string accountType);

        public Task<bool> UniqueGoalName(string goalName);

        public Task<bool> GoalExists(string goalName);
    }
}