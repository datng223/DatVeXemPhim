using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Handle.Email
{
    public class Validate
    {
        public static bool isValidEmail(string email)
        {
            var checkEmail = new EmailAddressAttribute();
            return checkEmail.IsValid(email);
        }
    }
}
