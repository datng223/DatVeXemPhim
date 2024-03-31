namespace DatVeXemPhim.Payloads.DataRequests.FoodRequest
{
    public class Request_AddFood
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string NameOfFood { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
