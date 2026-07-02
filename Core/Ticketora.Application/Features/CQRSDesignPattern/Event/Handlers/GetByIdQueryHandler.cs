using Microsoft.EntityFrameworkCore;
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
    public class GetByIdQueryHandler
    {
        private readonly TicketoraContext _context;
        public GetByIdQueryHandler(TicketoraContext context)
        {
            _context = context;
        }
        public async Task<GetByIdQueryResult> Handle(GetByIdEventQuery query)
        {
            var value = await _context.Events.Where(x => x.EventId == query.Id).Select(x => new GetByIdQueryResult
            {
                EventId = x.EventId,
                Title = x.Title,
                Description = x.Description,
                Location = x.Location,
                EventDate = x.EventDate,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                Status = x.Status
            }).FirstOrDefaultAsync();
            return value;
        }
    }
}
