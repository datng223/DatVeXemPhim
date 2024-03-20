using DatVeXemPhim.Payloads.DataRequests.CinemaRequest;
using DatVeXemPhim.Payloads.DataRequests.RoomRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<ResponseObject<DataResponseCinema>> AddCinema(Request_AddCinema request);
        Task<List<DataResponseRoom>> GetAlls();
        Task<ResponseObject<DataResponseRoom>> GetRoomById(int roomId);
        Task<ResponseObject<DataResponseRoom>> EditRoom(Request_EditCinema request);
        Task<string> RemoveCinema(int cinemaId);
    }
}
