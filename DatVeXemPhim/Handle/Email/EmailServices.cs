using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.OpenApi.Models;
using MimeKit;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Security;
using System.Text;
using Mailgun;
using DatVeXemPhim.Payloads.DataRequests.UserRequest;

namespace DatVeXemPhim.Handle.Email
{
    public class EmailServices : IEmailService
    {
        public async Task SendVerificationCode(Request_Register request,string verificationCode)
        {

            // Chuẩn bị email
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Beta cinemas", "tkodhunter@gmail.com"));
            email.To.Add(new MailboxAddress(request.Name, request.Email));
            email.Subject = "Mã xác minh";

            // Thêm nội dung email

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<h1>Mã xác minh của bạn là: {verificationCode}, mã này có hiệu lực là 10 phút</h1>";
            email.Body = bodyBuilder.ToMessageBody();

            // Cấu hình SMTP
            var smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtpClient.Authenticate("tkodhunter@gmail.com", "xjcy lxsr ikdx miyu");

            // Gửi email
            await smtpClient.SendAsync(email);

            // Giải phóng tài nguyên
            smtpClient.Dispose();
        }

        public async Task SendConfirmCode(Request_ForgotPassword request, string verificationCode)
        {

            // Chuẩn bị email
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Beta cinemas", "tkodhunter@gmail.com"));
            email.To.Add(new MailboxAddress("User", request.Email));
            email.Subject = "Mã xác minh";

            // Thêm nội dung email

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<h1>Mã xác minh của bạn là: {verificationCode}, mã này có hiệu lực là 10 phút</h1>";
            email.Body = bodyBuilder.ToMessageBody();

            // Cấu hình SMTP
            var smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtpClient.Authenticate("tkodhunter@gmail.com", "xjcy lxsr ikdx miyu");

            // Gửi email
            await smtpClient.SendAsync(email);

            // Giải phóng tài nguyên
            smtpClient.Dispose();
        }

        public static string GenerateVerificationCode()
        {
            // Sử dụng trình tạo số ngẫu nhiên an toàn để tạo mã
            var randomNumberGenerator = new SecureRandom();
            var code = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                code.Append(randomNumberGenerator.Next(0, 10));
            }
            return code.ToString();
        }
    }
}
