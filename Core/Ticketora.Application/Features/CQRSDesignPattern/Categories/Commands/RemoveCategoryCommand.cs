using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Commands
{
    public class RemoveCategoryCommand
    {
        public int CategoryId { get; set; }
    }
}
