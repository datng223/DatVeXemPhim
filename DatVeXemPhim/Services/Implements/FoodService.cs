using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.FoodRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DatVeXemPhim.Services.Implements
{
    public class FoodService : BaseService, IFoodService
    {
        private readonly ResponseObject<DataResponseFood> _responseObject;
        private readonly FoodConverter _converter;

        public FoodService(ResponseObject<DataResponseFood> responseObject, FoodConverter converter)
        {
            _responseObject = responseObject;
            _converter = converter;
        }

        public async Task<List<DataResponseFood>> GetAlls()
        {
            List<DataResponseFood> foodLst = _context.foods.Select(x => _converter.EntityToDTO(x)).ToList();
            return foodLst;
        }

        public async Task<ResponseObject<DataResponseFood>> GetFoodById(int foodId)
        {
            Food food = await _context.foods.SingleOrDefaultAsync(x => x.Id == foodId);
            if (food == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy món ăn");
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(food));
        }

        public async Task<ResponseObject<DataResponseFood>> AddFood(Request_AddFood request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.NameOfFood) || string.IsNullOrWhiteSpace(request.Image) || request.Price == null || request.Description == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin");
                }
                Food food = new Food
                {
                    NameOfFood = request.NameOfFood,
                    Image = request.Image,
                    Description = request.Description,
                    Price = request.Price,
                    IsActive = request.IsActive
                };
                _context.foods.Add(food);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Tạo món ăn thành công", _converter.EntityToDTO(food));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ResponseObject<DataResponseFood>> EditFood(Request_EditFood request)
        {
            try
            {
                var food = await _context.foods.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (food == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Món ăn không tồn tại");
                }
                food.NameOfFood = request.NameOfFood;
                food.Image = request.Image;
                food.Description = request.Description;
                food.Price = request.Price;
                food.IsActive = request.IsActive;
                _context.foods.Update(food);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Cập nhật thông tin món ăn thành công", _converter.EntityToDTO(food));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<string> RemoveFood(int foodId)
        {
            try
            {
                var food = await _context.foods.SingleOrDefaultAsync(x => x.Id == foodId);
                if (food == null)
                {
                    return "Món ăn không tồn tại";
                }
                _context.foods.Remove(food);
                _context.SaveChanges();
                return "Xóa món ăn thành công";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
