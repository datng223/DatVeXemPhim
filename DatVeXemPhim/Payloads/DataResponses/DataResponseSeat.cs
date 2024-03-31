using DatVeXemPhim.Entities;

namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponseSeat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string NameSeatStatus { get; set; }
        public string Line { get; set; }
        public int RoomId { get; set; }
        public bool? IsActive { get; set; } = true;
        public string NameSeatType { get; set; }
        public IEnumerable<Ticket>? tickets { get; set; }
    }
}
