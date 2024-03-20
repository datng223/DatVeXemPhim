using DatVeXemPhim.Entities;

namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponseRoom
    {
        public int                    Id { get; set; }
        public int                    Capacity { get; set; }
        public int                    Type { get; set; }
        public string                 Description { get; set; }
        public string                 NameOfCinema { get; set; }
        public string                 Code { get; set; }
        public string                 Name { get; set; }
        public bool?                  IsActive { get; set; } = true;
        public DataResponseCinema?    Cinema { get; set; }
        public IEnumerable<Seat>?     seats { get; set; }
        public IEnumerable<Schedule>? schedules { get; set; }
    }
}
