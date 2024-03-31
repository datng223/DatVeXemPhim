using DatVeXemPhim.Entities;

namespace DatVeXemPhim.Payloads.DataRequests.SeatRequest
{
    public class Request_EditSeat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int SeatStatusId { get; set; }
        public string Line { get; set; }
        public int RoomId { get; set; }
        public bool? IsActive { get; set; } = true;
        public int SeatTypeId { get; set; }
    }   
}
