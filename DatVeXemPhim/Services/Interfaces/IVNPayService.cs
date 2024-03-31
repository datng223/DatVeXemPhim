namespace DatVeXemPhim.Services.Interfaces
{
    public interface IVNPayService
    {
        Task<string> CreatePayment(int billId, HttpContext httpContext, int id);
        Task<string> VNPayReturn(IQueryCollection vnpayData);
    }
}
