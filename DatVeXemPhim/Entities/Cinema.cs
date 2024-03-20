namespace DatVeXemPhim.Entities
{
    public class Cinema : BaseEntity
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string NameOfCinema { get; set; }
        public bool? IsActive { get; set; } = true;
        public List<Room>? rooms { get; set; }
    }
}
