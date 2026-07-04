using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Queries;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Results;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.MediatorDesignPattern.Participants.Handlers
{
    public class GetByIdParticipantsQueryHandler : IRequestHandler<GetByIdParticipantsQuery, GetByIdParticipantsQueryResult>
    {
        private readonly TicketoraContext _context;

        public GetByIdParticipantsQueryHandler(TicketoraContext context)
        {
            _context = context;
        }
        public async Task<GetByIdParticipantsQueryResult> Handle(GetByIdParticipantsQuery request, CancellationToken cancellationToken)
        {
            var participant = await _context.Participants.Where(p => p.ParticipantId == request.Id).Select(p => new GetByIdParticipantsQueryResult
            {
                ParticipantId = p.ParticipantId,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Attended = p.Attended,
                CheckInDate = p.CheckInDate
            }).FirstOrDefaultAsync(cancellationToken);
            return participant;
        }
    }
}
