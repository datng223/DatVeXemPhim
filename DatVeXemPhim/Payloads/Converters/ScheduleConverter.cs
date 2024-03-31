using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class ScheduleConverter
    {
        private readonly AppDbContext _context;

        public ScheduleConverter()
        {
            _context = new AppDbContext();
        }
        public DataResponseSchedule EntityToDTO(Schedule schedule)
        {
            return new DataResponseSchedule
            {
                Id = schedule.Id,
                Price = schedule.Price,
                StartAt = schedule.StartAt,
                EndAt = schedule.EndAt,
                Code = schedule.Code,
                MovieName = _context.movies.SingleOrDefault(x => x.Id == schedule.MovieId).Name,
                Name = schedule.Name,
                RoomName = _context.rooms.SingleOrDefault(x => x.Id == schedule.MovieId).Name,
                IsActive = schedule.IsActive
            };
        }
    }
}
