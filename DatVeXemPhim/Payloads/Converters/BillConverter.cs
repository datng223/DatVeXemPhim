using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class BillConverter
    {
        private readonly AppDbContext _context;
        private readonly UserConverter _userConverter;
        private readonly BillTicketConverter _billTicketConverter;
        private readonly BillFoodConverter _billFoodConverter;
        private readonly PromotionConverter _promotionConverter;

        public BillConverter(AppDbContext context, UserConverter userConverter, PromotionConverter promotionConverter, BillTicketConverter billTicketConverter, BillFoodConverter billFoodConverter)
        {
            _context = context;
            _userConverter = userConverter;
            _promotionConverter = promotionConverter;
            _billTicketConverter = billTicketConverter;
            _billFoodConverter = billFoodConverter;
        }
        public DataResponseBill EntityToDTO(Bill bill)
        {
            return new DataResponseBill
            {
                Id = bill.Id,
                TotalMoney = bill.TotalMoney,
                TradingCode = bill.TradingCode,
                CreateTime = bill.CreateTime,
                Name = bill.Name,
                UpdateTime = bill.UpdateTime,
                Customer = _userConverter.EntityToDTO(_context.users.SingleOrDefault(x => x.Id == bill.CustomerId)),
                BillStatusName = _context.billStatuses.SingleOrDefault(x => x.Id == bill.BillStatusId)?.Name,
                Promotion = _promotionConverter.EntityToDTO(_context.promotions.SingleOrDefault(x => x.Id == bill.PromotionId)),
                BillTickets = _context.billTickets.Where(x => x.BillId == bill.Id).Select(x => _billTicketConverter.EntityToDTO(x)).ToList(),
                BillFoods = _context.billFoods.Where(x => x.BillId == bill.Id).Select(x => _billFoodConverter.EntityToDTO(x)).ToList()
            };
        }
    }
}
