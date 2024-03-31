using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class RoomConverter
    {
        private readonly AppDbContext _context;
        private readonly SeatConverter _seatConverter;
        private readonly ScheduleConverter _scheduleConverter;

        public RoomConverter(SeatConverter seatConverter, ScheduleConverter scheduleConverter)
        {
            _context = new AppDbContext();
            _seatConverter = seatConverter;
            _scheduleConverter = scheduleConverter;
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
                seats = _context.seats.Where(x => x.RoomId == room.Id).Select(x => _seatConverter.EntityToDTO(x)),
                schedules = _context.schedules.Where(x => x.RoomId == room.Id).Select(x => _scheduleConverter.EntityToDTO(x))
            };
        }
    }
}
