using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Entities
{
    public class Ticket : BaseEntity
    {
        public string Code { get; set; }
        public int ScheduleId { get; set; }
        public int SeatId { get; set; }
        public double PriceTicket { get; set; } = 90000;
        public bool? IsActive { get; set; } = true;
        public Schedule? Schedule { get; set; }
        public Seat? Seat { get; set; }
        public IEnumerable<BillTicket>? billTickets { get; set; }
    }
}
