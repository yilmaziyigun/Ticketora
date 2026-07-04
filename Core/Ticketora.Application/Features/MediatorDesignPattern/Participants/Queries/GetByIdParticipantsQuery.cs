using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Results;

namespace Ticketora.Application.Features.MediatorDesignPattern.Participants.Queries
{
    public class GetByIdParticipantsQuery:IRequest<GetByIdParticipantsQueryResult>
    {
        public int Id { get; set; }

        public GetByIdParticipantsQuery(int id)
        {
            Id = id;
        }
    }
}
