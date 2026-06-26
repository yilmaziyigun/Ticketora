using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Commands
{
    public class CreateCategoryCommand
    {
        public string Categoryname { get; set; }
    }
}
