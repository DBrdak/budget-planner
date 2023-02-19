using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }

        public IEnumerable<SavingDto> SavingsOut { get; set; }
        public IEnumerable<SavingDto> SavingsIn { get; set; }
        public IEnumerable<IncomeDto> Incomes { get; set; }
        public IEnumerable<ExpenditureDto> Expenditures { get; set; }
    }
}