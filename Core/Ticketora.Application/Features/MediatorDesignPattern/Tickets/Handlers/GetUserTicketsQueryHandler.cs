using MediatR;
using Microsoft.EntityFrameworkCore;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Queries;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Results;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Handlers
{
    public class GetUserTicketsQueryHandler : IRequestHandler<GetUserTicketsQuery, List<GetTicketQueryResult>>
    {
        private readonly TicketoraContext _context;

        public GetUserTicketsQueryHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetUserTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await (
                from t in _context.Tickets
                join e in _context.Events on t.EventId equals e.EventId
                where t.UserId == request.UserId
                orderby e.EventDate descending
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
