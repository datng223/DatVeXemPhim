using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.RoomRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using DatVeXemPhim.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Services.Implements
{
    public class RoomService : BaseService, IRoomService
    {
        private readonly ResponseObject<DataResponseRoom> _responseObject;
        private readonly RoomConverter _converter;
        public RoomService(RoomConverter converter)
        {
            _converter = converter;
            _responseObject = new ResponseObject<DataResponseRoom>();
        }

        public async Task<List<DataResponseRoom>> GetAlls()
        {
            List<DataResponseRoom> roomLst = _context.rooms.Select(x => _converter.EntityToDTO(x)).ToList();
            return roomLst;
        }

        public async Task<ResponseObject<DataResponseRoom>> GetRoomById(int roomId)
        {
            Room room = await _context.rooms.SingleOrDefaultAsync(x => x.Id == roomId);
            if (room == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy phòng");
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(room));
        }

        public async Task<ResponseObject<DataResponseRoom>> AddRoom(Request_AddRoom request)
        {
            try
            {
                int number;
                if (request.Capacity <= 0 || string.IsNullOrWhiteSpace(request.Description) || request.Code == null || request.CinemaId == null || request.Name == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin");
                }
                Room room = new Room
                {
                    Capacity = request.Capacity,
                    Type = request.Type,
                    Description = request.Description,
                    CinemaId = request.CinemaId,
                    Code = request.Code,
                    Name = request.Name,
    };
                _context.rooms.Add(room);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Tạo phòng thành công", _converter.EntityToDTO(room));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ResponseObject<DataResponseRoom>> EditRoom(Request_EditRoom request)
        {
            try
            {
                var room = await _context.rooms.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (room == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Phòng không tồn tại");
                }
                room.Capacity = request.Capacity;
                room.Description = request.Description;
                room.Type = request.Type;
                room.CinemaId = request.CinemaId;
                room.Code = request.Code;
                room.Name = request.Name;
                _context.rooms.Update(room);
                _context.SaveChanges();
                return _responseObject.ResponseSuccess("Cập nhật thông tin phòng thành công", _converter.EntityToDTO(room));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<string> RemoveRoom(int roomId)
        {
            try
            {
                var room = await _context.rooms.SingleOrDefaultAsync(x => x.Id == roomId);
                if (room == null)
                {
                    return "Phòng không tồn tại";
                }
                _context.rooms.Remove(room);
                _context.SaveChanges();
                return "Xóa phòng thành công";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
