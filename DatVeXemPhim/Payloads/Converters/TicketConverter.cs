using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class TicketConverter
    {
        private readonly AppDbContext _context;
        private readonly ScheduleConverter _scheduleConverter;
        private readonly SeatConverter _seatConverter;

        public TicketConverter(AppDbContext context, ScheduleConverter scheduleConverter, SeatConverter seatConverter)
        {
            _context = context;
            _scheduleConverter = scheduleConverter;
            _seatConverter = seatConverter;
        }
        public DataResponseTicket EntityToDTO(Ticket ticket)
        {
            return new DataResponseTicket
            {
                Id = ticket.Id,
                Code = ticket.Code,
                PriceTicket  = ticket.PriceTicket,
                IsActive = ticket.IsActive,
                Schedule = _scheduleConverter.EntityToDTO(_context.schedules.SingleOrDefault(x => x.Id == ticket.ScheduleId)),
                Seat = _seatConverter.EntityToDTO(_context.seats.SingleOrDefault(x => x.Id == ticket.SeatId))
            };
        }
    }
}
