using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }

        public IEnumerable<Savings> SavingsOut { get; set; }
        public IEnumerable<Savings> SavingsIn { get; set; }
        public IEnumerable<Savings> Transactions { get; set; }
    }
}