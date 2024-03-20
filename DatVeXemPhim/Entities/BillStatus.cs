namespace DatVeXemPhim.Entities
{
    public class BillStatus : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Bill> bills { get; set; }
    }
}
