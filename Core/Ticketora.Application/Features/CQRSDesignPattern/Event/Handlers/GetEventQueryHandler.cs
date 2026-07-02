using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Queries;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Results;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers
{
    public class GetEventQueryHandler
    {
        private readonly TicketoraContext _context;
        public GetEventQueryHandler(TicketoraContext context)
        {
            _context = context;
        }
        public async Task<List<GetEventQueryResult>> Handle()
        {
            var values = _context.Events.Select(x => new GetEventQueryResult
            {
                EventId = x.EventId,
                Title = x.Title,
                Description = x.Description,
                Location = x.Location,
                EventDate = x.EventDate,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                Status = x.Status
            }).ToList();
            return values;
        }
    }
}
