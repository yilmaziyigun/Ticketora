using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Commands;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers
{
    public class CreateEventCommandHandler
    {
        private readonly TicketoraContext _context;

        public CreateEventCommandHandler(TicketoraContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateEventCommand command)
        {
            var value = new Domain.Entities.Event()
            {
                Description = command.Description,
                EventDate = command.EventDate,
                Location = command.Location,
                ImageUrl = command.ImageUrl,
                Price = command.Price,
                Status = command.Status,
                Title = command.Title
            };
             _context.Events.Add(value);
            await _context.SaveChangesAsync();
        }
    }
}
