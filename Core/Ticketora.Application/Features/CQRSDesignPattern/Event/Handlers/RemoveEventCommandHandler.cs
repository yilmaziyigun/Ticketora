using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Commands;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers
{
    public class RemoveEventCommandHandler
    {
        private readonly TicketoraContext _context;

        public RemoveEventCommandHandler(TicketoraContext context)
        {
            _context = context;
        }
        public  async Task Handle(RemoveEventCommand command)
        {
            var value =  await _context.Events.FindAsync(command.Id);
            _context.Events.Remove(value);
                await _context.SaveChangesAsync();
        }
    }
}
