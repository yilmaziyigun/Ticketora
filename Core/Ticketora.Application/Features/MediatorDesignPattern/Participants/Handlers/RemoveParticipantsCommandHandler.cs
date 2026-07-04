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
    public class RemoveParticipantsCommandHandler : IRequestHandler<RemoveParticipantsCommand>
    {
        private readonly TicketoraContext _context;

        public RemoveParticipantsCommandHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveParticipantsCommand request, CancellationToken cancellationToken)
        {
            var value = _context.Participants.FirstOrDefault(x => x.ParticipantId == request.Id);
            _context.Participants.Remove(value);
           await _context.SaveChangesAsync();
        }
    }
}
