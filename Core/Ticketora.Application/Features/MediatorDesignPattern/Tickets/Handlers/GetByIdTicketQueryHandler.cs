using MediatR;
using Microsoft.EntityFrameworkCore;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Queries;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Results;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Handlers
{
    public class GetByIdTicketQueryHandler : IRequestHandler<GetByIdTicketQuery, GetByIdQueryResult>
    {
        private readonly TicketoraContext _context;

        public GetByIdTicketQueryHandler(TicketoraContext context)
        {
            _context = context;
        }

        public async Task<GetByIdQueryResult> Handle(GetByIdTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = await (
                from t in _context.Tickets
                join e in _context.Events on t.EventId equals e.EventId
                join p in _context.Participants on t.ParticipantId equals p.ParticipantId
                where t.TicketId == request.Id
                select new GetByIdQueryResult
                {
                    TicketId = t.TicketId,
                    TicketNumber = t.TicketNumber,
                    UserId = t.UserId,
                    Price = t.Price,
                    PurchaseDate = t.PurchaseDate,
                    IsUsed = t.IsUsed,
                    SeatNumber = t.SeatNumber,
                    QrCodeData = t.QrCodeData,
                    EventTitle = e.Title,
                    EventLocation = e.Location,
                    EventDate = e.EventDate,
                    ParticipantName = p.Name,
                    ParticipantSurname = p.Surname,
                    ParticipantEmail = p.Email,
                    ParticipantPhoneNumber = p.PhoneNumber
                }).FirstOrDefaultAsync(cancellationToken);

            return ticket;
        }
    }
}
