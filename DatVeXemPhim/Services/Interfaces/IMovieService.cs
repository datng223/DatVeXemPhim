using DatVeXemPhim.Payloads.DataRequests.MovieRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ResponseObject<DataResponseMovie>> AddMovie(Request_AddMovie request);
        Task<List<DataResponseMovie>> GetAlls();
        Task<List<DataResponseMovie>> GetMovieByCinema(int cinemaId);
        Task<List<DataResponseMovie>> GetMovieByRoom(int roomId);
        /*Task<List<DataResponseMovie>> GetMovieBySeat(int roomStatus);*/
        Task<ResponseObject<DataResponseMovie>> GetMovieById(int movieId);
        Task<ResponseObject<DataResponseMovie>> EditMovie(Request_EditMovie request);
        Task<string> RemoveMovie(int movieId);
        Task<List<DataResponseMovie>> GetFeaturedMovies();
    }
}
