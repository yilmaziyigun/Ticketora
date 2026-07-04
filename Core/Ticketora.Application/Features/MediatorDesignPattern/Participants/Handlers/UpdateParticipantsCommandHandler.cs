using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Commands;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.MediatorDesignPattern.Participants.Handlers
{
    public class UpdateParticipantsCommandHandler : IRequestHandler<UpdateParticipantsCommand>
    {
        private readonly TicketoraContext _context;
        public async Task Handle(UpdateParticipantsCommand request, CancellationToken cancellationToken)
        {
            var value = _context.Participants.FindAsync(request.ParticipantId);

                value.Result.Name = request.Name;
                value.Result.Surname = request.Surname;
                value.Result.Email = request.Email;
                value.Result.PhoneNumber = request.PhoneNumber;
                value.Result.Attended = request.Attended;
                value.Result.CheckInDate = request.CheckInDate;
               await _context.SaveChangesAsync();

        }
    }
}
