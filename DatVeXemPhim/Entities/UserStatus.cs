namespace DatVeXemPhim.Entities
{
    public class UserStatus : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public IEnumerable<User>? users { get; set; }
    }
}
