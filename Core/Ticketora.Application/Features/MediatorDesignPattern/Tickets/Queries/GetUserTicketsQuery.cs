using MediatR;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Results;

namespace Ticketora.Application.Features.MediatorDesignPattern.Tickets.Queries
{
    public class GetUserTicketsQuery : IRequest<List<GetTicketQueryResult>>
    {
        public GetUserTicketsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
