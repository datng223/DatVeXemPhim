using DatVeXemPhim.Entities;

namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponseTicket
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double PriceTicket { get; set; } = 90000;
        public bool? IsActive { get; set; } = true;
        public DataResponseSchedule? Schedule { get; set; }
        public DataResponseSeat? Seat { get; set; }
    }
}
