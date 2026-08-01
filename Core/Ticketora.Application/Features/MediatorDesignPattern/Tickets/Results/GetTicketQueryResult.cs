using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Results
{
    public class GetTicketQueryResult
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsUsed { get; set; }
        public string? SeatNumber { get; set; }
        public string EventTitle { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventDate { get; set; }
    }
}
