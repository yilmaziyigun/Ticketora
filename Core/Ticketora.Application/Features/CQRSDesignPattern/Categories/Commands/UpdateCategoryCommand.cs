using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Commands
{
    public class UpdateCategoryCommand
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
