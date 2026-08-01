using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ticketora.Application.Features.CQRSDesignPattern.Event.Commands
{
    public class CreateEventCommand : IRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime EventDate { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool Status { get; set; }

        public int Capacity { get; set; }

        public int CategoryId { get; set; }

    }
}
