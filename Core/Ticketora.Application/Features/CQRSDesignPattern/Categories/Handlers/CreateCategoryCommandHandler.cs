using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Commands;
using Ticketora.Domain.Entities;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers
{
    public class CreateCategoryCommandHandler
    {
        private readonly TicketoraContext _ticketoraContext;
        public CreateCategoryCommandHandler(TicketoraContext ticketoraContext)
        {
            _ticketoraContext = ticketoraContext;
        }
        public async Task Handle(CreateCategoryCommand command)
        {
            var category = new Category()
            {
                CategoryName = command.Categoryname
            };
            await _ticketoraContext.Categories.AddAsync(category);
            await _ticketoraContext.SaveChangesAsync();
        }
    }
}
