using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Entities
{
    public class Schedule : BaseEntity
    {
        public double Price { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string? Code { get; set; }
        public int MovieId { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public bool? IsActive { get; set; } = true;
        public IEnumerable<Ticket>? tickets { get; set; }
        public Movie? Movie { get; set; }
        public Room? Room { get; set; }
    }
}
