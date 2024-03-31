using DatVeXemPhim.Payloads.DataRequests.SeatRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface ISeatService
    {
        Task<ResponseObject<DataResponseSeat>> AddSeat(Request_AddSeat request);
        Task<List<DataResponseSeat>> GetAlls();
        Task<ResponseObject<DataResponseSeat>> GetSeatById(int seatId);
        Task<ResponseObject<DataResponseSeat>> EditSeat(Request_EditSeat request);
        Task<string> RemoveSeat(int seatId);
    }
}
