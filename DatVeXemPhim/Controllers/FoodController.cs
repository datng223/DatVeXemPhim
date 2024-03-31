using DatVeXemPhim.Payloads.DataRequests.FoodRequest;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    [Route("api/food")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAlls()
        {
            return Ok(_foodService.GetAlls());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetFoodById(int id)
        {
            var result = await _foodService.GetFoodById(id);
            return Ok(result);
        }

        [HttpPost("add-food")]
        public async Task<IActionResult> AddFood([FromBody] Request_AddFood request)
        {
            var res = await _foodService.AddFood(request);
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

        [HttpPut("edit-food")]
        public async Task<IActionResult> EditFood([FromBody] Request_EditFood request)
        {
            var res = await _foodService.EditFood(request);
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

        [HttpDelete("remove-food")]
        public async Task<IActionResult> RemoveFood([FromQuery] int foodId)
        {
            var res = await _foodService.RemoveFood(foodId);
            return Ok(res);
        }
    }
}
