using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class MovieConverter
    {
        private readonly AppDbContext _context;
        private readonly ScheduleConverter _scheduleConverter;

        public MovieConverter(ScheduleConverter scheduleConverter)
        {
            _context = new AppDbContext();
            _scheduleConverter = scheduleConverter;
        }
        public DataResponseMovie EntityToDTO(Movie movie)
        {
            return new DataResponseMovie
            {
                Id = movie.Id,
                MovieDuration = movie.MovieDuration,
                EndTime  = movie.EndTime,
                PremiereDate = movie.PremiereDate,
                Description = movie.Description,
                Director = movie.Director,
                Image = movie.Image,
                HeroImage = movie.HeroImage,
                Language = movie.Language,
                Name = movie.Name,
                NameMovieType = _context.movieTypes.SingleOrDefault(x => x.Id == movie.MovieTypeId).MovieTypeName,
                RateDescription = _context.rates.SingleOrDefault(x => x.Id == movie.RateId).Description,
                Trailer = movie.Trailer,
                IsActive = movie.IsActive,
                schedules = _context.schedules.Where(x => x.MovieId == movie.Id).Select(x => _scheduleConverter.EntityToDTO(x))
            };
        }
    }
}
