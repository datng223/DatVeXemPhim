using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataRequests.CinemaRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;

namespace DatVeXemPhim.Services.Implements
{
    public class CinemaService : ICinemaService
    {
        private readonly ResponseObject<DataResponseCinema> _responseObject;
        private readonly CinemaConverter _converter;

        public Task<ResponseObject<DataResponseCinema>> AddCinema(Request_AddCinema request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<DataResponseRoom>> EditRoom(Request_EditCinema request)
        {
            throw new NotImplementedException();
        }

        public Task<List<DataResponseRoom>> GetAlls()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<DataResponseRoom>> GetRoomById(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveCinema(int cinemaId)
        {
            throw new NotImplementedException();
        }
    }
}
