using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.SeatRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Services.Implements
{
    public class SeatService : BaseService, ISeatService
    {
        private readonly ResponseObject<DataResponseSeat> _responseObject;
        private readonly SeatConverter _converter;

        public SeatService(ResponseObject<DataResponseSeat> responseObject, SeatConverter converter)
        {
            _responseObject = responseObject;
            _converter = converter;
        }

        public async Task<List<DataResponseSeat>> GetAlls()
        {
            List<DataResponseSeat> seatLst = _context.seats.Select(x => _converter.EntityToDTO(x)).ToList();
            return seatLst;
        }

        public async Task<ResponseObject<DataResponseSeat>> GetSeatById(int seatId)
        {
            Seat seat = await _context.seats.SingleOrDefaultAsync(x => x.Id == seatId);
            if (seat == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy ghế");
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(seat));
        }

        public async Task<ResponseObject<DataResponseSeat>> AddSeat(Request_AddSeat request)
        {
            try
            {                
                if (request.Number <= 0 || string.IsNullOrWhiteSpace(request.Line) || request.SeatStatusId == null || request.SeatTypeId == null || request.RoomId == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin");
                }
                Seat seat = new Seat
                {
                    Number = request.Number,
                    Line = request.Line,
                    SeatStatusId = request.SeatStatusId,
                    RoomId = request.RoomId,
                    SeatTypeId = request.SeatTypeId,
                    IsActive = request.IsActive
                };
                _context.seats.Add(seat);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Thêm ghế thành công", _converter.EntityToDTO(seat));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ResponseObject<DataResponseSeat>> EditSeat(Request_EditSeat request)
        {
            try
            {
                var seat = await _context.seats.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (seat == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Ghế không tồn tại");
                }
                seat.Number = request.Number;
                seat.Line = request.Line;
                seat.SeatStatusId = request.SeatStatusId;
                seat.RoomId = request.RoomId;
                seat.SeatTypeId = request.SeatTypeId;
                seat.IsActive = request.IsActive;
                _context.seats.Update(seat);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Cập nhật thông tin ghế thành công", _converter.EntityToDTO(seat));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<string> RemoveSeat(int seatId)
        {
            try
            {
                var seat = await _context.seats.SingleOrDefaultAsync(x => x.Id == seatId);
                if (seat == null)
                {
                    return "Ghế không tồn tại";
                }
                _context.seats.Remove(seat);
                _context.SaveChanges();
                return "Xóa ghế thành công";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
