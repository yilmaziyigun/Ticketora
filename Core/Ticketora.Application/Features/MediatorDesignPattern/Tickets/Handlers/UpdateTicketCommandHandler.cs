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
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
    {
        private readonly TicketoraContext _context;

        public UpdateTicketCommandHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.TicketId);
            ticket.TicketNumber = request.TicketNumber;
            ticket.Price = request.Price;
            ticket.PurchaseDate = request.PurchaseDate;
            ticket.IsUsed = request.IsUsed;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
