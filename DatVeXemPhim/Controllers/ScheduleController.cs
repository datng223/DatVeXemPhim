using DatVeXemPhim.Payloads.DataRequests.ScheduleRequest;
using DatVeXemPhim.Services.Implements;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    [Route("api/schedule")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAlls()
        {
            return Ok(_scheduleService.GetAlls());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var result = await _scheduleService.GetScheduleById(id);
            return Ok(result);
        }

        [HttpGet("get-schedule/{movieId}/{cinemaId}/{roomId}")]
        public async Task<IActionResult> GetSchedulesByRoomMovieAndCinema(int movieId, int cinemaId, int roomId)
        {
            var result = await _scheduleService.GetSchedulesByRoomMovieAndCinema(movieId, cinemaId, roomId);
            return Ok(result);
        }

        [HttpPost("add-schedule")]
        public async Task<IActionResult> AddSchedule([FromBody] Request_AddSchedule request)
        {
            var res = await _scheduleService.AddSchedule(request);
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

        [HttpPut("edit-schedule")]
        public async Task<IActionResult> EditSchedule([FromBody] Request_EditSchedule request)
        {
            var res = await _scheduleService.EditSchedule(request);
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

        [HttpDelete("remove-schedule")]
        public async Task<IActionResult> RemoveSchedule([FromQuery] int scheduleId)
        {
            var res = await _scheduleService.RemoveSchedule(scheduleId);
            return Ok(res);
        }
    }
}
