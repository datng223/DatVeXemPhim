using DatVeXemPhim.DataContext;
using DatVeXemPhim.Entities;
using DatVeXemPhim.Handle.Email;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.UserRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DatVeXemPhim.Services.Implements
{
    public class UserService : BaseService, IUserService
    {
        private readonly ResponseObject<DataResponseUser> _responseObject;
        private readonly UserConverter _converter;
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<DataResponseToken> _responseTokenObject;
        private readonly IEmailService _emailService;

        public UserService(IConfiguration configuration, IEmailService emailService)
        {
            _converter = new UserConverter();
            _responseObject = new ResponseObject<DataResponseUser>();
            _configuration = configuration;
            _responseTokenObject = new ResponseObject<DataResponseToken>();
            _emailService = emailService;
        }

        public async Task<ResponseObject<DataResponseUser>> Register(Request_Register request)
        {
            if(string.IsNullOrWhiteSpace(request.Username) 
                || string.IsNullOrWhiteSpace(request.Email)
                || string.IsNullOrWhiteSpace(request.Name)
                || string.IsNullOrWhiteSpace(request.PhoneNumber)
                || string.IsNullOrWhiteSpace(request.Password)
                )
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin");
            }

            if (_context.users.Any(x => x.Email.Equals(request.Email)))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại trên hệ thống");
            }
            if (_context.users.Any(x => x.Username.Equals(request.Username)))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Tên tài khoản đã tồn tại trên hệ thống");
            }
            if(!Validate.isValidEmail(request.Email))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Định dạng email không hợp lệ");
            }
            var user = new User();            
            user.Point = 0;
            user.Email = request.Email;
            user.Name = request.Name;
            user.PhoneNumber = request.PhoneNumber;
            user.Password = BCryptNet.HashPassword(request.Password);
            user.Username = request.Username;
            user.RoleId = 3;
            user.UserStatusId = 1;
            user.RankCustomerId = 2;
            _context.Add(user);
            await _context.SaveChangesAsync();

            // Lưu mã xác thực
            var verification = new ConfirmEmail
            {
                UserId = user.Id,
                RequiredTime = DateTime.Now,
                ExpiredTime = DateTime.Now.AddMinutes(10),
                ConfirmCode = EmailServices.GenerateVerificationCode(),
            };
            _context.Add(verification);
            await _context.SaveChangesAsync();
            await _emailService.SendVerificationCode(request, verification.ConfirmCode);
            return _responseObject.ResponseSuccess("Đăng ký tài khoản thành công. Vui lòng kiểm tra email của bạn để xác minh.", _converter.EntityToDTO(user));
        }


        public async Task<ResponseObject<DataResponseUser>> VerifyEmail(Request_ConfirmEmail request)
        {
            ConfirmEmail confirmEmail = _context.confirmEmails.Where(x => x.ConfirmCode.Equals(request.VerificationCode)).FirstOrDefault();
            if (confirmEmail == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Mã xác nhận không chính xác");
            }
            if (confirmEmail.ExpiredTime < DateTime.Now)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận hết hạn");
            }
            User user = _context.users.SingleOrDefault(x => x.Id == confirmEmail.UserId);
            user.UserStatusId = 3;
            _context.confirmEmails.Remove(confirmEmail);
            _context.users.Update(user);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Xác nhận tài khoản thành công", _converter.EntityToDTO(user));
        }
        public DataResponseToken RenewAccessToken(Request_RenewAccessToken request)
        {
            // Xác thực mã làm mới
            var refreshToken = _context.refreshTokens.SingleOrDefault(x => x.Token == request.RefreshToken);

            if (refreshToken == null || refreshToken.ExpireTime < DateTime.Now)
            {
                // Xử lý mã làm mới không hợp lệ hoặc đã hết hạn
                return null;
            }

            // Lấy người dùng liên quan đến mã làm mới
            var user = _context.users.SingleOrDefault(x => x.Id == refreshToken.UserId);

            if (user == null)
            {
                // Xử lý người dùng không hợp lệ
                return null;
            }

            // Tạo mã truy cập mới
            var newAccessToken = GenerateAccessToken(user);

            // Cập nhật mã làm mới (tùy chọn: tạo mã làm mới mới)
            refreshToken.ExpireTime = DateTime.Now.AddDays(1);
            _context.refreshTokens.Update(refreshToken);
            _context.SaveChanges();

            return newAccessToken;
        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using(var item = RandomNumberGenerator.Create())
            {
                item.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public DataResponseToken GenerateAccessToken(User user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

            var role = _context.roles.SingleOrDefault(x => x.Id == user.RoleId);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, role.Code)
                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = JwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = JwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ExpireTime = DateTime.Now.AddDays(1),
                UserId = user.Id,
            };
            _context.refreshTokens.Add(rf);
            _context.SaveChanges();
            DataResponseToken result = new DataResponseToken
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
            return result;
        }

        public ResponseObject<DataResponseToken> Login(Request_Login request)
        {
            var user = _context.users.SingleOrDefault(x => x.Username.Equals(request.Username));
            if(string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return _responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin");
            }
            bool checkPass = BCryptNet.Verify(request.Password, user.Password);
            if(!checkPass)
            {
                return _responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu không chính xác");
            }
            return _responseTokenObject.ResponseSuccess("Đăng nhập thành công", GenerateAccessToken(user));
        }

        public IQueryable<DataResponseUser> GetAll()
        {
            var result = _context.users.Select(x => _converter.EntityToDTO(x));
            return result;
        }
        public async Task<string> ForgotPassword(Request_ForgotPassword request)
        {
            User user = await _context.users.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (user is null)
            {
                return "Email không tồn tại trong hệ thống";
            }
            else
            {
                var confirms = _context.confirmEmails.Where(x => x.UserId == user.Id).ToList();
                _context.confirmEmails.RemoveRange(confirms);
                await _context.SaveChangesAsync();
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    UserId = user.Id,
                    IsConfirm = false,
                    ExpiredTime = DateTime.Now.AddMinutes(10),
                    ConfirmCode = EmailServices.GenerateVerificationCode()
                };
                await _context.confirmEmails.AddAsync(confirmEmail);
                await _context.SaveChangesAsync();
                await _emailService.SendConfirmCode(request, confirmEmail.ConfirmCode);
                return "Gửi mã xác nhận về email thành công, vui lòng kiểm tra email";
            }
        }

        public async Task<string> ChangePassword(int userId, Request_ChangePassword request)
        {
            User user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId);
            bool checkOldPassword = BCryptNet.Verify(request.OldPassword, user.Password);
            if (!checkOldPassword)
            {
                return "Mật khẩu không chính xác";
            }
            if (string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.NewPassword))
            {
                return "Vui lòng điền đầy đủ thông tin";
            }
            if (!request.NewPassword.Equals(request.ConfirmNewPassword))
            {
                return "Mật khẩu không trùng khớp";
            }
            user.Password = BCryptNet.HashPassword(request.NewPassword);
            await _context.SaveChangesAsync();
            return "Đổi mật khẩu thành công";
        }

        public async Task<ResponseObject<DataResponseUser>> CreateNewPassword(Request_CreateNewPassword request)
        {
            ConfirmEmail confirmEmail = await _context.confirmEmails.SingleOrDefaultAsync(x => x.ConfirmCode.Equals(request.ConfirmCode));
            if (confirmEmail is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Mã xác nhận không hợp lệ! Vui lòng kiểm tra lại");
            }
            User user = await _context.users.SingleOrDefaultAsync(x => x.Id == confirmEmail.UserId);
            if (user is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại");
            }
            if (confirmEmail.ExpiredTime < DateTime.Now)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận đã hết hạn");
            }
            if (!request.NewPassword.Equals(request.ConfirmNewPassword))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu không trùng khớp");
            }
            user.Password = BCryptNet.HashPassword(request.NewPassword);
            _context.users.Update(user);
            _context.confirmEmails.Remove(confirmEmail);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Tạo mật khẩu mới thành công", _converter.EntityToDTO(user));
        }

        public async Task<ResponseObject<DataResponseUser>> GetUserById(int id)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại");
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(user));
        }
    }
}
