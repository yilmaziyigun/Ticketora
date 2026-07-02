using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, List<GetTicketQueryResult>>
    {
        private readonly TicketoraContext _context;

        public GetTicketQueryHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _context.Tickets.Select(t => new GetTicketQueryResult
            {
                TicketId = t.TicketId,
                TicketNumber = t.TicketNumber,
                Price = t.Price,
                PurchaseDate = t.PurchaseDate,
                IsUsed = t.IsUsed
            }).ToListAsync();
            return tickets;
        }
    }
}
