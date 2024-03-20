using DatVeXemPhim.Payloads.DataRequests.RoomRequest;
using DatVeXemPhim.Services.Implements;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    [Route("api/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAlls()
        {
            return Ok(_roomService.GetAlls());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetRoomById(int id)
        {
            return Ok(_roomService.GetRoomById(id));
        }

        [HttpPost("add-room")]
        public async Task<IActionResult> AddRoom([FromBody] Request_AddRoom request)
        {
            var res = await _roomService.AddRoom(request);
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

        [HttpPut("edit-room")]
        public async Task<IActionResult> EditRoom([FromBody] Request_EditRoom request)
        {
            var res = await _roomService.EditRoom(request);
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

        [HttpDelete("remove-room")]
        public IActionResult RemoveRoom([FromQuery] int roomId)
        {
            var res = _roomService.RemoveRoom(roomId);
            return Ok(res);
        }
    }
}
