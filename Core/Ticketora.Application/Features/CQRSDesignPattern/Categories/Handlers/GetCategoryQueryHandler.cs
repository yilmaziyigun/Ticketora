using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Results;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers
{
    public class GetCategoryQueryHandler
    {
        private readonly TicketoraContext _ticketoraContext;
        public GetCategoryQueryHandler(TicketoraContext ticketoraContext)
        {
            _ticketoraContext = ticketoraContext;
        }
        public async Task<List<GetCategoryQueryResult>> Handle()
        {
           var categories = await _ticketoraContext.Categories.Select(c => new GetCategoryQueryResult
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToListAsync();
            return categories;
        }
    }
}
