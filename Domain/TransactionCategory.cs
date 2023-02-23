using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TransactionCategory
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}