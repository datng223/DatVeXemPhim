using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Entities
{
    public class ConfirmEmail : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime RequiredTime { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string ConfirmCode { get; set; }
        public bool? IsConfirm { get; set; } = false;
        public User? User { get; set; }
    }
}
