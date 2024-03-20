namespace DatVeXemPhim.Entities
{
    public class SeatType : BaseEntity
    {
        public string NameType { get; set; }
        public IEnumerable<Seat>? seats { get; set; }
    }
}
