using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Queries;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Results;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Handlers
{
    public class GetByIdTicketQueryHandler : IRequestHandler<GetByIdTicketQuery, GetByIdQueryResult>
    {
        private readonly TicketoraContext _context;
        public async Task<GetByIdQueryResult> Handle(GetByIdTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = _context.Tickets.Where(t => t.TicketId == request.Id).
                Select(t => new GetByIdQueryResult
                {
                TicketId = t.TicketId,
                TicketNumber = t.TicketNumber,
                Price = t.Price,
                PurchaseDate = t.PurchaseDate,
                IsUsed = t.IsUsed
            }).FirstOrDefault();

            return ticket;
        }
    }
}
