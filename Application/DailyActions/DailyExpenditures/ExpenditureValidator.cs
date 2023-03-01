using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DailyActions.DailyExpenditures
{
    public class ExpenditureValidator : AbstractValidator<ExpenditureDto>
    {
        // Musimy zwalidować:
        // Amount -> większe niż zero
        // Title -> wymagany
        // Date -> mniejszy lub równy niż aktualny czas
        // Category -> tu musisz w ValidationExtension stworzyć metodę
        // która sprawdza czy podana kategoria występuje w tabeli FutureTransactions
        // AccountName -> tu musisz wykorzystać metodę z ValidationExtension, AccountExists()
        // Do każdej property musimy dać jakiś message dla clienta
    }
}