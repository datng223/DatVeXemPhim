using DatVeXemPhim.Entities;

namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponseBill
    {
        public int Id { get; set; }
        public double? TotalMoney { get; set; }
        public string TradingCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }
        public DataResponseUser Customer { get; set; }
        public DataResponsePromotion Promotion { get; set; }
        public string BillStatusName { get; set; }
        public List<DataResponseBillTicket> BillTickets { get; set; }
        public List<DataResponseBillFood> BillFoods { get; set; }
    }
}
