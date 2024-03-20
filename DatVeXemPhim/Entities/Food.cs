namespace DatVeXemPhim.Entities
{
    public class Food : BaseEntity
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string NameOfFood { get; set; }
        public bool? IsActive { get; set; } = true;
        public IEnumerable<BillFood>? billFoods { get; set; }
    }
}
