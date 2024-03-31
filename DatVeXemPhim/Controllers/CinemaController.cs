using DatVeXemPhim.Payloads.DataRequests.CinemaRequest;
using DatVeXemPhim.Services.Implements;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    [Route("api/cinema")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAlls()
        {
            return Ok(_cinemaService.GetAlls());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetCinemaById(int id)
        {
            var result = await _cinemaService.GetCinemaById(id);
            return Ok(result);
        }
        

        [HttpGet("get-cinema-by-movie/{movieId}")]
        public async Task<IActionResult> GetCinemasByMovie(int movieId)
        {
            var result = await _cinemaService.GetCinemasByMovie(movieId);
            return Ok(result);
        }

        [HttpPost("add-cinema")]
        public async Task<IActionResult> AddCinema([FromBody] Request_AddCinema request)
        {
            var res = await _cinemaService.AddCinema(request);
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

        [HttpPut("edit-cinema")]
        public async Task<IActionResult> EditCinema([FromBody] Request_EditCinema request)
        {
            var res = await _cinemaService.EditCinema(request);
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

        [HttpDelete("remove-cinema")]
        public async Task<IActionResult> RemoveCinema([FromQuery] int cinemaId)
        {
            var res = await _cinemaService.RemoveCinema(cinemaId);
            return Ok(res);
        }
    }
}
