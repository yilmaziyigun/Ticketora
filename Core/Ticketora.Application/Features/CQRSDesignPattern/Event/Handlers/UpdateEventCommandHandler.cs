using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Commands;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers
{
    public class UpdateEventCommandHandler
    {
        private readonly TicketoraContext _context;
        public UpdateEventCommandHandler(TicketoraContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateEventCommand command)
        {
            var value = await _context.Events.FindAsync(command.EventId);
            value.Title = command.Title;
            value.Description = command.Description;
            value.Location = command.Location;
            value.EventDate = command.EventDate;
            value.ImageUrl = command.ImageUrl;
            value.Price = command.Price;
            value.Status = command.Status;
            await _context.SaveChangesAsync();
        }
    }
}
