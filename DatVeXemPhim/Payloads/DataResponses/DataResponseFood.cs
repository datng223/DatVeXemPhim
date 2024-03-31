using System.ComponentModel;

namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponseFood
    {
        public int Id { get; set; }
        public string NameOfFood { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
