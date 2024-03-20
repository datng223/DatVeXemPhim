using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;
using MimeKit.Text;

namespace DatVeXemPhim.Payloads.Converters
{
    public class RoomConverter
    {
        private readonly AppDbContext _context;
        private readonly RoomConverter _roomConverter;
        private readonly CinemaConverter _cinemaConverter;
        //private readonly SeatConverter _seatConverter;
        //private readonly ScheduleConverter _scheduleConverter;

        public RoomConverter(RoomConverter roomConverter, CinemaConverter cinemaConverter)
        {
            _context = new AppDbContext();
            _roomConverter = roomConverter;
            _cinemaConverter = cinemaConverter;
            //_seatConverter = seatConverter;
            //_scheduleConverter = scheduleConverter;
        }
        public DataResponseRoom EntityToDTO(Room room)
        {
            return new DataResponseRoom
            {
                Id = room.Id,
                Capacity = room.Capacity,
                Type = room.Type,
                Description = room.Description,
                Code = room.Code,
                NameOfCinema = _context.cinemas.SingleOrDefault(x => x.Id == room.CinemaId).NameOfCinema,
                IsActive = room.IsActive,
                Cinema = _cinemaConverter.EntityToDTO(room.Cinema),
                //seats = _context.seats.Where(x => x.RoomId == room.Id).Select(x => _seatConverter.EntityToDTO(x)),
                //schedules = _context.schedules.Where(x => x.RoomId == room.Id).Select(x => _scheduleConverter.EntityToDTO(x)),
            };
        }
    }
}
