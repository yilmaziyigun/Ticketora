using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Commands;
using Ticketora.Persistence.Context;
using EventEntity = Ticketora.Domain.Entities.Event;

namespace Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
    {
        private readonly TicketoraContext _context;

        public CreateEventCommandHandler(TicketoraContext context)
        {
            _context = context;
        }
        public Task Handle(CreateEventCommand command)
        {
            return Handle(command, CancellationToken.None);
        }

        public async Task Handle(CreateEventCommand command, CancellationToken cancellationToken)
        {
            var value = new EventEntity
            {
                Description = command.Description,
                EventDate = command.EventDate,
                Location = command.Location,
                ImageUrl = command.ImageUrl,
                Price = command.Price,
                Status = command.Status,
                Title = command.Title,
                Capacity = command.Capacity,
                CategoryId = command.CategoryId
            };
            _context.Events.Add(value);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
