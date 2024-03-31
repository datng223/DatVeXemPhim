using DatVeXemPhim.Payloads.DataRequests.RoomRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface IRoomService
    {
        Task<List<DataResponseRoom>> GetAlls();
        Task<ResponseObject<DataResponseRoom>> GetRoomById(int roomId);
        Task<ResponseObject<DataResponseRoom>> EditRoom(Request_EditRoom request);
        Task<ResponseObject<DataResponseRoom>> AddRoom(Request_AddRoom request);
        Task<string> RemoveRoom(int roomId);
        Task<List<DataResponseRoom>> GetRoomsByMovie(int movieId, int cinemaId);
    }
}
