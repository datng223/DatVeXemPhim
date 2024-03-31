using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class FoodConverter
    {
        public DataResponseFood EntityToDTO(Food food)
        {
            return new DataResponseFood
            {
                Id = food.Id,
                NameOfFood = food.NameOfFood,
                Image = food.Image,
                Description = food.Description,
                Price = food.Price,
                IsActive = food.IsActive
            };
        }
    }
}
