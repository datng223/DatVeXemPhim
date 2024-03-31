using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class SeatConverter
    {
        private readonly AppDbContext _context;

        public SeatConverter()
        {
            _context = new AppDbContext();
        }
        public DataResponseSeat EntityToDTO(Seat seat)
        {
            return new DataResponseSeat
            {
                Id = seat.Id,
                Number = seat.Number,
                NameSeatStatus = _context.seatStatuses.SingleOrDefault(x => x.Id == seat.SeatStatusId).NameStatus,
                Line  = seat.Line,
                RoomId = seat.RoomId,
                IsActive = seat.IsActive,
                NameSeatType = _context.SeatTypes.SingleOrDefault(x => x.Id == seat.SeatTypeId).NameType
            };
        }
    }
}
