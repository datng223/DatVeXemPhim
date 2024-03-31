using DatVeXemPhim.Payloads.DataRequests.ScheduleRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<ResponseObject<DataResponseSchedule>> AddSchedule(Request_AddSchedule request);
        Task<List<DataResponseSchedule>> GetAlls();
        Task<ResponseObject<DataResponseSchedule>> GetScheduleById(int scheduleId);
        Task<ResponseObject<DataResponseSchedule>> EditSchedule(Request_EditSchedule request);
        Task<string> RemoveSchedule(int scheduleId);
        Task<List<DataResponseSchedule>> GetSchedulesByRoomMovieAndCinema(int movieId, int cinemaId, int roomId);
    }
}
