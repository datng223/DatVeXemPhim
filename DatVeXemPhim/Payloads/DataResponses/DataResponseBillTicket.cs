using DatVeXemPhim.Entities;

namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponseBillTicket
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int Quantity { get; set; }
        public DataResponseTicket Ticket { get; set; }
    }
}
