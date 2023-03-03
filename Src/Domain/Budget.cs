using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Budget
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<Saving> Savings { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<FutureSaving> FutureSavings { get; set; }
        public IEnumerable<FutureTransaction> FutureTransactions { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<TransactionCategory> TransactionCategories { get; set; }
    }
}