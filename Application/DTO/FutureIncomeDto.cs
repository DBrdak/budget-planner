using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class FutureIncomeDto
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Frequency { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public string AccountName { get; set; }
    }
}