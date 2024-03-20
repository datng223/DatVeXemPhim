namespace DatVeXemPhim.Entities
{
    public class RankCustomer : BaseEntity
    {
        public int Point { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; } = true;
        public IEnumerable<User>? users { get; set; }
        public IEnumerable<Promotion>? promotions { get; set; }
    }
}
