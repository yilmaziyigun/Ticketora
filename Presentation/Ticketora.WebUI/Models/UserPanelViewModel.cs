using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Results;

namespace Ticketora.WebUI.Models
{
    public class UserPanelViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<GetTicketQueryResult> ActiveTickets { get; set; } = new();
        public List<GetTicketQueryResult> PastTickets { get; set; } = new();
    }
}
