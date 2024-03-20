namespace DatVeXemPhim.Payloads.DataRequests.UserRequest
{
    public class Request_CreateNewPassword
    {
        public string ConfirmCode { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
