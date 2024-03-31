using DatVeXemPhim.Payloads.DataRequests.MailRequest;
using DatVeXemPhim.Payloads.DataRequests.UserRequest;

namespace DatVeXemPhim.Handle.Email
{
    public interface IEmailService
    {
        Task SendVerificationCode(Request_Register request, string verificationCode);
        Task SendConfirmCode(Request_ForgotPassword request, string verificationCode);
        Task SendMail(EmailTo emailTo);

    }
}
