using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DailyActions.DailySavings
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid SavingId { get; set; }
        }

        //Place for validator

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext dataContext)
            {
                _context = dataContext;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var saving = await _context.Savings.FindAsync(request.SavingId);

                if (saving == null)
                    return null;

                // Tu ważne:
                // Mamy dwie opcje:
                // 1. Completed amount robić po stronie zapytania, wtedy spowalniamy proces ale zyskujemy czystość
                // oraz zajebiście wygodny kod
                // (oczywiście są sposoby na zrobienie tego w wydajny sposób,
                // ale mogłoby być ciężko na ten moment, możemy to dać jako cel w razie rozwijania API)
                // 2. Completed amount robić po stronie logiki, a przechowywać w bazie danych, zyskujemy dużo wyższą wydajność
                // ale tracimy czystość i elastyczność kodu

                // Osobiście preferuję 2. więc musiałbyś wtedy zrobić myk z odjęciem completed amount w odpowiednim Future Saving i Goal

                _context.Savings.Remove(saving);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while saving changes on database");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}