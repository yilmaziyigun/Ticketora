using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketora.Domain.Entities
{
    public class Participant
    {
        public int ParticipantId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool Attended { get; set; }

        public DateTime? CheckInDate { get; set; }

    }
}
