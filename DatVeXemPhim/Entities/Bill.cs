using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Entities
{
    public class Bill : BaseEntity
    {
        public double? TotalMoney { get; set; }
        public string TradingCode { get; set; }
        public DateTime CreateTime { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }
        public int PromotionId { get; set; }
        public int BillStatusId { get; set; }
        public bool IsActive { get; set; }
        public User? Customer { get; set; }
        public IEnumerable<BillTicket>? billTickets { get; set; }
        public IEnumerable<BillFood>? billFoods { get; set; }
        public Promotion? Promotion { get; set; }
        public BillStatus? BillStatus { get; set; }
    }
}
