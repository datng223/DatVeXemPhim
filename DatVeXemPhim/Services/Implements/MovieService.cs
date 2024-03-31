using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.MovieRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace DatVeXemPhim.Services.Implements
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly ResponseObject<DataResponseMovie> _responseObject;
        private readonly MovieConverter _converter;

        public MovieService(ResponseObject<DataResponseMovie> responseObject, MovieConverter converter)
        {
            _responseObject = responseObject;
            _converter = converter;
        }

        public async Task<List<DataResponseMovie>> GetAlls()
        {
            List<DataResponseMovie> movieLst = _context.movies.Select(x => _converter.EntityToDTO(x)).ToList();
            return movieLst;
        }

        public async Task<ResponseObject<DataResponseMovie>> GetMovieById(int movieId)
        {
            Movie movie = await _context.movies.SingleOrDefaultAsync(x => x.Id == movieId);
            if (movie == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy phim");
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(movie));
        }

        public async Task<ResponseObject<DataResponseMovie>> AddMovie(Request_AddMovie request)
        {
            try
            {                
                if (request.MovieDuration <= 0 || string.IsNullOrWhiteSpace(request.Description) || request.EndTime == null || request.PremiereDate == null || request.Director == null || request.Image == null || request.HeroImage == null || request.Language == null || request.MovieTypeId == null || request.Name == null || request.RateId == null || request.Trailer == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin");
                }
                Movie movie = new Movie
                {
                    MovieDuration = request.MovieDuration,
                    EndTime = request.EndTime,
                    PremiereDate = request.PremiereDate,
                    Description = request.Description,
                    Director = request.Director,
                    Image = request.Image,
                    HeroImage = request.HeroImage,
                    Language = request.Language,
                    MovieTypeId = request.MovieTypeId,
                    Name = request.Name,
                    RateId = request.RateId,
                    Trailer = request.Trailer,
                    IsActive = request.IsActive
                };
                _context.movies.Add(movie);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Thêm phim thành công", _converter.EntityToDTO(movie));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ResponseObject<DataResponseMovie>> EditMovie(Request_EditMovie request)
        {
            try
            {
                var movie = await _context.movies.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (movie == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Phim không tồn tại");
                }
                movie.MovieDuration = request.MovieDuration;
                movie.EndTime = request.EndTime;
                movie.PremiereDate = request.PremiereDate;
                movie.Description = request.Description;
                movie.Director = request.Director;
                movie.Image = request.Image;
                movie.HeroImage = request.HeroImage;
                movie.Language = request.Language;
                movie.MovieTypeId = request.MovieTypeId;
                movie.Name = request.Name;
                movie.RateId = request.RateId;
                movie.Trailer = request.Trailer;
                movie.IsActive = request.IsActive;
                _context.movies.Update(movie);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Cập nhật thông tin phim thành công", _converter.EntityToDTO(movie));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<string> RemoveMovie(int movieId)
        {
            try
            {
                var movie = await _context.movies.SingleOrDefaultAsync(x => x.Id == movieId);
                if (movie == null)
                {
                    return "Phim không tồn tại";
                }
                _context.movies.Remove(movie);
                _context.SaveChanges();
                return "Xóa phim thành công";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public async Task<List<DataResponseMovie>> GetFeaturedMovies()
        {
            var movies = await _context.movies
            .OrderByDescending(m => m.schedules.Sum(s => s.tickets.Sum(t => t.billTickets.Sum(x => x.Quantity)))).ToListAsync();
            var responseMovies = movies.Select(x => _converter.EntityToDTO(x)).ToList();

            return responseMovies;
        }

        public async Task<List<DataResponseMovie>> GetMovieByCinema(int cinemaId)
        {
            var schedules = await _context.schedules
                .Where(s => s.Room.CinemaId == cinemaId)
                .ToListAsync();

            var movies = schedules.Select(s => s.Movie)
                .Distinct()
                .ToList();
            var responseMovies = movies.Select(x => _converter.EntityToDTO(x)).ToList();
            return responseMovies;
        }

        public async Task<List<DataResponseMovie>> GetMovieByRoom(int roomId)
        {
            var schedules = _context.schedules.Where(s => s.RoomId == roomId).ToList();
            var movies = schedules.Select(s => s.Movie).Distinct().ToList();
            var responseMovies = movies.Select(x => _converter.EntityToDTO(x)).ToList();
            return responseMovies;
        }

        /*public Task<List<DataResponseMovie>> GetMovieBySeat(int roomStatus)
        {
            throw new NotImplementedException();
        }*/
    }
}
