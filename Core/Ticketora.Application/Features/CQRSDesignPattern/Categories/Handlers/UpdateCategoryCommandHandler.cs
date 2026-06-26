using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Commands;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers
{
    public class UpdateCategoryCommandHandler
    {
        private readonly TicketoraContext _ticketoraContext;
        public UpdateCategoryCommandHandler(TicketoraContext ticketoraContext)
        {
            _ticketoraContext = ticketoraContext;
        }
        public async Task Handle(UpdateCategoryCommand command)
        {
            var category = await _ticketoraContext.Categories.FindAsync(command.CategoryId);
             category.CategoryName = command.CategoryName;
                await _ticketoraContext.SaveChangesAsync();
        }
    }
}
