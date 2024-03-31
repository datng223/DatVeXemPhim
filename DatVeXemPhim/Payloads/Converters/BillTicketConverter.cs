using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataResponses;

namespace DatVeXemPhim.Payloads.Converters
{
    public class BillTicketConverter
    {
        private readonly AppDbContext _context;
        private readonly TicketConverter _ticketConverter;

        public BillTicketConverter(AppDbContext context, TicketConverter ticketConverter)
        {
            _context = context;
            _ticketConverter = ticketConverter;
        }

        public DataResponseBillTicket EntityToDTO(BillTicket billTicket)
        {
            return new DataResponseBillTicket
            {
                Id = billTicket.Id,
                Quantity = billTicket.Quantity,
                BillId = billTicket.BillId,
                Ticket = _ticketConverter.EntityToDTO(_context.tickets.SingleOrDefault(t => t.Id == billTicket.TicketId))    };
        }
    }
}
