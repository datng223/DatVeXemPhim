using DatVeXemPhim.Entities;

namespace DatVeXemPhim.Payloads.DataRequests.RoomRequest
{
    public class Request_AddRoom
    {
        public int    Capacity    { get; set; }
        public int    Type        { get; set; }
        public string Description { get; set; }
        public int    CinemaId    { get; set; }
        public string Code        { get; set; }
        public string Name        { get; set; }
    }
}
