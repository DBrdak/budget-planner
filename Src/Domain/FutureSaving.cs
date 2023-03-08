using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FutureSaving
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }

        public double CompletedAmount { get; set; }

        public DateTime Date { get; set; }

        public Goal Goal { get; set; }

        public Guid FromAccountId { get; set; }
        public Account FromAccount { get; set; }
        public Guid ToAccountId { get; set; }
        public Account ToAccount { get; set; }

        public IEnumerable<Saving> CompletedSavings { get; set; } = new List<Saving>();

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }

        public void SetCompletedAmount() => CompletedAmount = CompletedSavings.Select(ct => ct.Amount).Sum();
    }
}