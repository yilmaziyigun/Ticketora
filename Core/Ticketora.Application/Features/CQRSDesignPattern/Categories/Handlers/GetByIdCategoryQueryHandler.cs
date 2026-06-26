using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Results;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers
{
    public class GetByIdCategoryQueryHandler
    {
        private readonly TicketoraContext _ticketoraContext;

        public GetByIdCategoryQueryHandler(TicketoraContext ticketoraContext)
        {
            _ticketoraContext = ticketoraContext;
        }
        public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQueryResult query)
        {
            var category = await _ticketoraContext.Categories.FindAsync(query.CategoryId);
            return new GetCategoryByIdQueryResult
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }
    }
}
