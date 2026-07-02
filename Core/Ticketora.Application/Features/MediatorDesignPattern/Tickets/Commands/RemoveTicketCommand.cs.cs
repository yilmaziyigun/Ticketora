using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Commands
{
    public class RemoveTicketCommand:IRequest
    {
        public int Id { get; set; }
        public RemoveTicketCommand(int id)
        {
            Id = id;
        }
    }
}
