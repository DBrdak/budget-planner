using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}