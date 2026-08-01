using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var values = await (
                from x in _context.Events
                join c in _context.Categories on x.CategoryId equals c.CategoryId into categoryGroup
                from category in categoryGroup.DefaultIfEmpty()
                orderby x.EventDate
                select new GetEventQueryResult
                {
                    EventId = x.EventId,
                    Title = x.Title,
                    Description = x.Description,
                    Location = x.Location,
                    EventDate = x.EventDate,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Status = x.Status,
                    Capacity = x.Capacity,
                    SoldTicketCount = _context.Tickets.Count(t => t.EventId == x.EventId),
                    CategoryId = x.CategoryId,
                    CategoryName = category == null ? "Genel" : category.CategoryName
                }).ToListAsync();

            return values;
        }
    }
}
