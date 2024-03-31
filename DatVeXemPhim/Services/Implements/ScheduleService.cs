using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.ScheduleRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace DatVeXemPhim.Services.Implements
{
    public class ScheduleService : BaseService, IScheduleService
    {
        private readonly ResponseObject<DataResponseSchedule> _responseObject;
        private readonly ScheduleConverter _converter;

        public ScheduleService(ResponseObject<DataResponseSchedule> responseObject, ScheduleConverter converter)
        {
            _responseObject = responseObject;
            _converter = converter;
        }

        public async Task<List<DataResponseSchedule>> GetAlls()
        {
            List<DataResponseSchedule> scheduleLst = _context.schedules.Select(x => _converter.EntityToDTO(x)).ToList();
            return scheduleLst;
        }

        public async Task<ResponseObject<DataResponseSchedule>> GetScheduleById(int scheduleId)
        {
            Schedule schedule = await _context.schedules.SingleOrDefaultAsync(x => x.Id == scheduleId);
            if (schedule == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy lịch chiếu");
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(schedule));
        }

        private bool IsScheduleOverlap(int roomId, DateTime startAt, DateTime endAt)
        {
            return _context.schedules.Any(x =>
                x.RoomId == roomId &&
                ((x.StartAt >= startAt && x.StartAt < endAt) ||
                 (x.EndAt > startAt && x.EndAt <= endAt) ||
                 (startAt >= x.StartAt && endAt <= x.EndAt)));
        }

        public async Task<ResponseObject<DataResponseSchedule>> AddSchedule(Request_AddSchedule request)
        {
            try
            {                
                if (request.Price <= 0 || string.IsNullOrWhiteSpace(request.Code) || request.StartAt == null || request.EndAt == null || request.MovieId == null || string.IsNullOrWhiteSpace(request.Name) || request.RoomId == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin");
                }
                if (IsScheduleOverlap(request.RoomId, request.StartAt, request.EndAt))
                {
                    throw new InvalidOperationException("Lịch chiếu trùng lặp với lịch chiếu hiện có.");
                }

                Schedule schedule = new Schedule
                {
                    Price = request.Price,
                    StartAt = request.StartAt,
                    EndAt = request.EndAt,
                    Code = request.Code,
                    MovieId = request.MovieId,
                    Name = request.Name,
                    RoomId = request.RoomId,
                    IsActive = request.IsActive
                };
                _context.schedules.Add(schedule);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Thêm lịch chiếu thành công", _converter.EntityToDTO(schedule));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ResponseObject<DataResponseSchedule>> EditSchedule(Request_EditSchedule request)
        {
            try
            {
                var schedule = await _context.schedules.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (schedule == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Lịch chiếu không tồn tại");
                }
                if (IsScheduleOverlap(request.RoomId, request.StartAt, request.EndAt))
                {
                    throw new InvalidOperationException("Lịch chiếu trùng lặp với lịch chiếu hiện có.");
                }
                schedule.Price = request.Price;
                schedule.StartAt = request.StartAt;
                schedule.EndAt = request.EndAt;
                schedule.Code = request.Code;
                schedule.MovieId = request.MovieId;
                schedule.Name = request.Name;
                schedule.RoomId = request.RoomId;
                schedule.IsActive = request.IsActive;
                _context.schedules.Update(schedule);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Cập nhật thông tin lịch chiếu thành công", _converter.EntityToDTO(schedule));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<string> RemoveSchedule(int scheduleId)
        {
            try
            {
                var schedule = await _context.schedules.SingleOrDefaultAsync(x => x.Id == scheduleId);
                if (schedule == null)
                {
                    return "Lịch chiếu không tồn tại";
                }
                _context.schedules.Remove(schedule);
                _context.SaveChanges();
                return "Xóa lịch chiếu thành công";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public async Task<List<DataResponseSchedule>> GetSchedulesByRoomMovieAndCinema(int movieId, int cinemaId, int roomId)
        {
            var schedules = await _context.schedules
                .Where(s => s.RoomId == roomId && s.MovieId == movieId && s.Room.CinemaId == cinemaId)
                .ToListAsync();

            var responseSchedules = schedules.Select(s => _converter.EntityToDTO(s)).ToList();

            return responseSchedules;
        }
    }
}
