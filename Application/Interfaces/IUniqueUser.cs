using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUniqueUser
    {
        public Task<bool> UniqueUsername(string newBudgetName);

        public Task<bool> UniqueEmail(string newEmail);

        public Task<bool> UniqueBudgetName(string newUsername);
    }
}