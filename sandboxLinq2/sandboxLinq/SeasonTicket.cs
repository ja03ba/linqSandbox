using System;

namespace sandboxLinq
{
    class SeasonTicket
    {
        public DateTime Expires { get; set; }
        public TicketClass Class { get; set; }
        public enum TicketClass
        {
            VIP, Standart
        }
    }
}
