using DatVeXemPhim.Payloads.DataRequests.FoodRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface IFoodService
    {
        Task<ResponseObject<DataResponseFood>> AddFood(Request_AddFood request);
        Task<List<DataResponseFood>> GetAlls();
        Task<ResponseObject<DataResponseFood>> GetFoodById(int foodId);
        Task<ResponseObject<DataResponseFood>> EditFood(Request_EditFood request);
        Task<string> RemoveFood(int foodId);
    }
}
