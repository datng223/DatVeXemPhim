using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.CinemaRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Services.Implements
{
    public class CinemaService : BaseService, ICinemaService
    {
        private readonly ResponseObject<DataResponseCinema> _responseObject;
        private readonly CinemaConverter _converter;

        public CinemaService(ResponseObject<DataResponseCinema> responseObject, CinemaConverter converter)
        {
            _responseObject = responseObject;
            _converter = converter;
        }

        public async Task<List<DataResponseCinema>> GetAlls()
        {
            List<DataResponseCinema> cinemaLst = _context.cinemas.Select(x => _converter.EntityToDTO(x)).ToList();
            return cinemaLst;
        }

        public async Task<ResponseObject<DataResponseCinema>> GetCinemaById(int cinemaId)
        {
            Cinema cinema = await _context.cinemas.SingleOrDefaultAsync(x => x.Id == cinemaId);
            if (cinema == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy rạp");
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(cinema));
        }

        public async Task<ResponseObject<DataResponseCinema>> AddCinema(Request_AddCinema request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Address) || string.IsNullOrWhiteSpace(request.Description) || request.Code == null || request.NameOfCinema == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin");
                }
                Cinema cinema = new Cinema
                {
                    Address = request.Address,
                    Description = request.Description,
                    Code = request.Code,
                    NameOfCinema = request.NameOfCinema,
                    IsActive = request.IsActive
                };
                _context.cinemas.Add(cinema);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Tạo rạp thành công", _converter.EntityToDTO(cinema));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ResponseObject<DataResponseCinema>> EditCinema(Request_EditCinema request)
        {
            try
            {
                var cinema = await _context.cinemas.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (cinema == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Rạp không tồn tại");
                }
                cinema.Address = request.Address;
                cinema.Description = request.Description;
                cinema.Code = request.Code;
                cinema.NameOfCinema = request.NameOfCinema;
                cinema.IsActive = request.IsActive;
                _context.cinemas.Update(cinema);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Cập nhật thông tin rạp thành công", _converter.EntityToDTO(cinema));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<string> RemoveCinema(int cinemaId)
        {
            try
            {
                var cinema = await _context.cinemas.SingleOrDefaultAsync(x => x.Id == cinemaId);
                if (cinema == null)
                {
                    return "Rạp không tồn tại";
                }
                _context.cinemas.Remove(cinema);
                _context.SaveChanges();
                return "Xóa rạp thành công";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public async Task<List<DataResponseCinema>> GetCinemasByMovie(int movieId)
        {
            // Lấy danh sách các lịch chiếu của bộ phim
            var schedules = _context.schedules.Where(s => s.MovieId == movieId).ToList();

            // Lấy danh sách các phòng chiếu từ các lịch chiếu
            var roomIds = schedules.Select(s => s.RoomId).Distinct().ToList();
            var rooms = _context.rooms.Where(r => roomIds.Contains(r.Id)).ToList();

            // Lấy danh sách các rạp từ các phòng chiếu
            var cinemaIds = rooms.Select(r => r.CinemaId).Distinct().ToList();
            var cinemas = _context.cinemas.Where(c => cinemaIds.Contains(c.Id)).ToList();
            var responseCinemas = cinemas.Select(x => _converter.EntityToDTO(x)).ToList();
            return responseCinemas;
        }
    }
}
