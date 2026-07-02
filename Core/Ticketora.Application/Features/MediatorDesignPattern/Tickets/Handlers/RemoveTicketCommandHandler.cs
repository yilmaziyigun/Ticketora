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
    public class RemoveTicketCommandHandler:IRequestHandler<RemoveTicketCommand>
    {
        private readonly TicketoraContext _context;

        public RemoveTicketCommandHandler(TicketoraContext context)
        {
            _context = context;
        }
        public async Task Handle(RemoveTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.Id);
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
        }
    }
}
