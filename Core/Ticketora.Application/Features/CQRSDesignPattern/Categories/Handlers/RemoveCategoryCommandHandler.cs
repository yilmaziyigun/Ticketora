using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Commands;
using Ticketora.Persistence.Context;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers
{
    public class RemoveCategoryCommandHandler
    {
        private readonly TicketoraContext _ticketoraContext;
        public RemoveCategoryCommandHandler(TicketoraContext ticketoraContext)
        {
            _ticketoraContext = ticketoraContext;
        }
        public async Task Handle(RemoveCategoryCommand command)
        {
            var category = await _ticketoraContext.Categories.FindAsync(command.CategoryId);
            _ticketoraContext.Categories.Remove(category);
             await _ticketoraContext.SaveChangesAsync();
        }
    }
}
