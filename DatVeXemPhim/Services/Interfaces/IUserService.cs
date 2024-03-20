using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataRequests.UserRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseObject<DataResponseUser>> Register(Request_Register request);
        Task<ResponseObject<DataResponseUser>> VerifyEmail(Request_ConfirmEmail request);
        DataResponseToken GenerateAccessToken(User user);
        DataResponseToken RenewAccessToken(Request_RenewAccessToken request);
        ResponseObject<DataResponseToken> Login(Request_Login request);
        IQueryable<DataResponseUser> GetAll();
        Task<ResponseObject<DataResponseUser>> GetUserById(int id);
        Task<string> ForgotPassword(Request_ForgotPassword request);
        Task<string> ChangePassword(int userId, Request_ChangePassword request);
        Task<ResponseObject<DataResponseUser>> CreateNewPassword(Request_CreateNewPassword request);
    }
}
