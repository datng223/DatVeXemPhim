﻿using DatVeXemPhim.Entities;

namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponsePromotion
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; } = true;
        public string RankCustomerName { get; set; }
    }
}
