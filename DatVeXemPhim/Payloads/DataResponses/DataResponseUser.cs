namespace DatVeXemPhim.Payloads.DataResponses
{
    public class DataResponseUser
    {
        public int Id { get; set; }
        public int? Point { get; set; } = 0;
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string? RankCustomerName { get; set; }
        public string UserStatusName { get; set; }
        public bool? IsActive { get; set; } = true;
        public string RoleName { get; set; }
    }
}
