using DatVeXemPhim.Entities;
using System.ComponentModel;

namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponseCinema
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string NameOfCinema { get; set; }
        [DefaultValue(true)]
        public bool? IsActive { get; set; }
        public List<DataResponseRoom>? rooms { get; set; }
    }
}
