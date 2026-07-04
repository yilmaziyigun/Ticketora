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
    public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, List<GetParticipantsQueryResult>>
    {
        private readonly TicketoraContext _context;

        public GetParticipantsQueryHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task<List<GetParticipantsQueryResult>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
        {   
            var participants = await _context.Participants.Select(p => new GetParticipantsQueryResult
            {
                ParticipantId = p.ParticipantId,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Attended = p.Attended,
                CheckInDate = p.CheckInDate
            }).ToListAsync();
            return participants;
        }
    }
}
