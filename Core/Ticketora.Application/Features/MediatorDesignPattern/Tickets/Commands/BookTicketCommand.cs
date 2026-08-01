using MediatR;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Commands
{
    public class BookTicketCommand : IRequest<int>
    {
        public int EventId { get; set; }

        public string? UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string? SeatNumber { get; set; }
    }
}
