using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Commands;
using Ticketora.Domain.Entities;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.MediatorDesignPattern.Participants.Handlers
{
    public class CreateParticipantsCommandHandler:IRequestHandler<CreateParticipantsCommand>
    {
        private readonly TicketoraContext _context;

        public CreateParticipantsCommandHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateParticipantsCommand request, CancellationToken cancellationToken)
        {
            var participant = new Participant
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Attended = request.Attended,
                CheckInDate = request.CheckInDate
            };
            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();
        }
    }
}
