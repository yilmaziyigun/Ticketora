using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Application.Features.CQRSDesignPattern.Event.Results
{
    public class GetByIdQueryResult
    {
        public int EventId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime EventDate { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool Status { get; set; }

        public int Capacity { get; set; }

        public int SoldTicketCount { get; set; }

        public int RemainingCapacity => Capacity <= 0 ? 0 : Math.Max(Capacity - SoldTicketCount, 0);

    }
}
