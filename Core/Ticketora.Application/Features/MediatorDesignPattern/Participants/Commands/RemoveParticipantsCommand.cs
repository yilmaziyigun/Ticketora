using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Application.Features.MediatorDesignPattern.Participants.Commands
{
    public class RemoveParticipantsCommand:IRequest
    {
      public int Id { get; set; }

        public RemoveParticipantsCommand(int id)
        {
            Id = id;
        }
    }
}
