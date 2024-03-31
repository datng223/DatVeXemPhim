using DatVeXemPhim.Payloads.DataRequests.CinemaRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<ResponseObject<DataResponseCinema>> AddCinema(Request_AddCinema request);
        Task<List<DataResponseCinema>> GetAlls();
        Task<ResponseObject<DataResponseCinema>> GetCinemaById(int cinemaId);
        Task<ResponseObject<DataResponseCinema>> EditCinema(Request_EditCinema request);
        Task<string> RemoveCinema(int cinemaId);
        Task<List<DataResponseCinema>> GetCinemasByMovie(int movieId);
    }
}
