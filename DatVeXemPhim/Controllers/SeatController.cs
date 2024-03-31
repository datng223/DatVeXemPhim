using DatVeXemPhim.Payloads.DataRequests.SeatRequest;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    [Route("api/seat")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAlls()
        {
            return Ok(_seatService.GetAlls());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetSeatById(int id)
        {
            var result = await _seatService.GetSeatById(id);
            return Ok(result);
        }

        [HttpPost("add-seat")]
        public async Task<IActionResult> AddSeat([FromBody] Request_AddSeat request)
        {
            var res = await _seatService.AddSeat(request);
            if (res.Status == StatusCodes.Status200OK)
            {
                return Ok(res);
            }
            else if (res.Status == StatusCodes.Status400BadRequest)
            {
                return BadRequest(res);
            }
            else
            {
                return StatusCode(res.Status, res);
            }
        }

        [HttpPut("edit-seat")]
        public async Task<IActionResult> EditSeat([FromBody] Request_EditSeat request)
        {
            var res = await _seatService.EditSeat(request);
            if (res.Status == StatusCodes.Status200OK)
            {
                return Ok(res);
            }
            else if (res.Status == StatusCodes.Status400BadRequest)
            {
                return BadRequest(res);
            }
            else
            {
                return StatusCode(res.Status, res);
            }
        }

        [HttpDelete("remove-seat")]
        public async Task<IActionResult> RemoveSeat([FromQuery] int seatId)
        {
            var res = await _seatService.RemoveSeat(seatId);
            return Ok(res);
        }
    }
}
