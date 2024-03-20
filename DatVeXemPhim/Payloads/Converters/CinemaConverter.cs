using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class CinemaConverter
    {
        private readonly AppDbContext _context;
        private readonly RoomConverter _roomConverter;
        public CinemaConverter(RoomConverter roomConverter)
        {
            _context = new AppDbContext();
            _roomConverter = roomConverter;
        }
        public DataResponseCinema EntityToDTO(Cinema cinema)
        {
            return new DataResponseCinema
            {
                Id = cinema.Id,
                Address = cinema.Address,
                Description = cinema.Description,
                Code = cinema.Code,
                NameOfCinema = cinema.NameOfCinema,
                IsActive = cinema.IsActive,
                rooms = _context.rooms.Where(x => x.CinemaId == cinema.Id).Select(x => _roomConverter.EntityToDTO(x)).ToList(),
            };
        }
    }
}
