using MediatR;
using Microsoft.EntityFrameworkCore;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Commands;
using Ticketora.Application.Interfaces;
using Ticketora.Domain.Entities;
using Ticketora.Persistence.Context;

public class BookTicketCommandHandler : IRequestHandler<BookTicketCommand, int>
{
    private readonly TicketoraContext _context;
    private readonly ITicketNumberGenerator _ticketNumberGenerator;

    public BookTicketCommandHandler(
        TicketoraContext context,
        ITicketNumberGenerator ticketNumberGenerator)
    {
        _context = context;
        _ticketNumberGenerator = ticketNumberGenerator;
    }

    public async Task<int> Handle(BookTicketCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _context.Events
            .FirstOrDefaultAsync(x => x.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
            throw new Exception("Etkinlik bulunamadı.");

        if (!eventEntity.Status)
            throw new Exception("Etkinlik aktif değil.");

        if (eventEntity.EventDate < DateTime.Now)
            throw new Exception("Etkinlik tarihi geçmiş.");

        var ticketCount = await _context.Tickets
            .CountAsync(x => x.EventId == request.EventId, cancellationToken);

        if (eventEntity.Capacity > 0 && ticketCount >= eventEntity.Capacity)
            throw new Exception("Etkinlik kapasitesi dolmuştur.");

        var participant = await _context.Participants
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (participant == null)
        {
            participant = new Participant
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Attended = false
            };

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync(cancellationToken);
        }

        var ticket = new Ticket
        {
            EventId = eventEntity.EventId,
            ParticipantId = participant.ParticipantId,
            TicketNumber = _ticketNumberGenerator.Generate(),
            UserId = request.UserId ?? string.Empty,
            Price = eventEntity.Price,
            PurchaseDate = DateTime.Now,
            IsUsed = false,
            SeatNumber = request.SeatNumber,
            QrCodeData = $"{eventEntity.EventId}|{participant.Email}|{DateTime.UtcNow:yyyyMMddHHmmss}"
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync(cancellationToken);

        return ticket.TicketId;
    }
}
