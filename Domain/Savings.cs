using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Savings
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public Goal Goal { get; set; }
    }
}