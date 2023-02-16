using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FutureSavings
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string Frequency { get; set; }
        public DateTime Date { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public Goal Goal { get; set; }

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}