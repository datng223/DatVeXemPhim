using DatVeXemPhim.Payloads.DataRequests.BillRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;

namespace DatVeXemPhim.Services.Interfaces
{
    public interface IBillService
    {
        /*Task<ResponseObject<DataResponseBill>> CreateBill(Request_CreateBill request);*/
        Task<string> RemoveBill(int billId);
    }
}
