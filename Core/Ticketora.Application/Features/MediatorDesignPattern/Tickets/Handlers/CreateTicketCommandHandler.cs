using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Commands;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Handlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
    {
        private readonly TicketoraContext _context;

        public CreateTicketCommandHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Domain.Entities.Ticket
            {
                TicketNumber = request.TicketNumber,
                Price = request.Price,
                PurchaseDate = request.PurchaseDate,
                IsUsed = request.IsUsed
            };
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
