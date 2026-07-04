using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Results;

namespace Ticketora.Application.Features.MediatorDesignPattern.Participants.Queries
{
    public class GetParticipantsQuery: IRequest<List<GetParticipantsQueryResult>>
    {
    }
}
