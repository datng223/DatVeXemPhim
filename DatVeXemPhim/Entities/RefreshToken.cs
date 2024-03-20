using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
