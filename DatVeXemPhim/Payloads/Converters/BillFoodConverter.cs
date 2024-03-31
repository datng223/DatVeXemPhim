using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class BillFoodConverter
    {
        private readonly AppDbContext _context;

        public BillFoodConverter(AppDbContext context)
        {
            _context = context;
        }

        public DataResponseBillFood EntityToDTO(BillFood billFood)
        {
            return new DataResponseBillFood
            {
                Id = billFood.Id,
                Quantity = billFood.Quantity,
                BillId = billFood.BillId,
                FoodName = _context.foods.SingleOrDefault(x => x.Id == billFood.FoodId).NameOfFood,
                Price = _context.foods.SingleOrDefault(x => x.Id == billFood.FoodId).Price
            };
        }
    }
}
