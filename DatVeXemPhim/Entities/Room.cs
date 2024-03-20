using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Entities
{
    public class Room : BaseEntity
    {
        public int Capacity { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int CinemaId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; } = true;
        public Cinema? Cinema { get; set; }
        public IEnumerable<Seat>? seats { get; set; }
        public IEnumerable<Schedule>? schedules { get; set; }
    }
}
