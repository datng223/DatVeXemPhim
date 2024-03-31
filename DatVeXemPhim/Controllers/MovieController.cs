using DatVeXemPhim.Payloads.DataRequests.MovieRequest;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    [Route("api/movie")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAlls()
        {
            return Ok(_movieService.GetAlls());
        }

        [HttpGet("get-featured-movies")]
        public IActionResult GetFeaturedMovies()
        {
            return Ok(_movieService.GetFeaturedMovies());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var result = await _movieService.GetMovieById(id);
            return Ok(result);
        }

        [HttpGet("get-movie-by-cinema/{cinemaId}")]
        public async Task<IActionResult> GetMovieByCinema(int cinemaId)
        {
            var result = await _movieService.GetMovieByCinema(cinemaId);
            return Ok(result);
        }

        [HttpGet("get-movie-by-room/{roomId}")]
        public async Task<IActionResult> GetMovieByRoom(int roomId)
        {
            var result = await _movieService.GetMovieByRoom(roomId);
            return Ok(result);
        }

        [HttpPost("add-movie")]
        public async Task<IActionResult> AddMovie([FromBody] Request_AddMovie request)
        {
            var res = await _movieService.AddMovie(request);
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

        [HttpPut("edit-movie")]
        public async Task<IActionResult> EditMovie([FromBody] Request_EditMovie request)
        {
            var res = await _movieService.EditMovie(request);
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

        [HttpDelete("remove-movie")]
        public async Task<IActionResult> RemoveMovie([FromQuery] int movieId)
        {
            var res = await _movieService.RemoveMovie(movieId);
            return Ok(res);
        }
    }
}
