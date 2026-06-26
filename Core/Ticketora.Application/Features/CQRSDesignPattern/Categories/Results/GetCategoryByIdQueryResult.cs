using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Application.Features.CQRSDesignPattern.Categories.Results
{
    public class GetCategoryByIdQueryResult
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
