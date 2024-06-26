﻿using DatVeXemPhim.Entities;
using DatVeXemPhim.Payloads.DataRequests.RoomRequest;
using DatVeXemPhim.Services.Implements;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    [Route("api/room")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAlls()
        {
            return Ok(_roomService.GetAlls());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var result = await _roomService.GetRoomById(id);
            return Ok(result);
        }

        [HttpGet("get-room-by-movie/{movieId}/{cinemaId}")]
        public async Task<IActionResult> GetRoomsByMovie(int movieId, int cinemaId)
        {
            var result = await _roomService.GetRoomsByMovie(movieId, cinemaId);
            return Ok(result);
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
        public async Task<IActionResult> RemoveRoom([FromQuery] int roomId)
        {
            var res = await _roomService.RemoveRoom(roomId);
            return Ok(res);
        }
    }
}
