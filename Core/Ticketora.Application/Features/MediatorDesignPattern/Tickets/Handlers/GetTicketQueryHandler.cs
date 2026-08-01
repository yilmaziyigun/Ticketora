using MediatR;
using Microsoft.EntityFrameworkCore;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Queries;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Results;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Handlers
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, List<GetTicketQueryResult>>
    {
        private readonly TicketoraContext _context;

        public GetTicketQueryHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var tickets = await (
                from t in _context.Tickets
                join e in _context.Events on t.EventId equals e.EventId
                select new GetTicketQueryResult
                {
                    TicketId = t.TicketId,
                    TicketNumber = t.TicketNumber,
                    UserId = t.UserId,
                    Price = t.Price,
                    PurchaseDate = t.PurchaseDate,
                    IsUsed = t.IsUsed,
                    SeatNumber = t.SeatNumber,
                    EventTitle = e.Title,
                    EventLocation = e.Location,
                    EventDate = e.EventDate
                }).ToListAsync(cancellationToken);

            return tickets;
        }
    }
}
