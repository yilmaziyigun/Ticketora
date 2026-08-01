using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketora.Application.Interfaces;

namespace Ticketora.Persistence.Services
{
    public class TicketNumberGenerator : ITicketNumberGenerator
    {
        public string Generate()
        {
            return $"TCK-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }
    }
}
