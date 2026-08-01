using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Domain.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public string TicketNumber { get; set; }

        public string UserId { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public bool IsUsed { get; set; }

        public string? SeatNumber { get; set; }

        public string? QrCodeData { get; set; }

        public int EventId { get; set; }
        public int ParticipantId { get; set; }
    }
}
