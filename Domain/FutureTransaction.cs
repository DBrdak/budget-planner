using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FutureTransaction
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }

        public double CompletedAmount { get; set; } = 0;
        public DateTime Date { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public IEnumerable<Transaction> CompletedTransactions { get; set; } = new List<Transaction>();

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}