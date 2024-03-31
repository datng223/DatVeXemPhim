namespace DatVeXemPhim.Payloads.DataRequests.BillRequest
{
    public class Request_CreateBill
    {
        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public int ScheduleId { get; set; }
        public int[] SelectedFoodIds { get; set; }
        public int[] SelectedTicketIds { get; set; }
        public int CustomerId { get; set; }
    }
}
