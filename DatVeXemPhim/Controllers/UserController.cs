using DatVeXemPhim.DataContext;
using DatVeXemPhim.Payloads.DataRequests.UserRequest;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseUser> _responseObject;
        public UserController(IUserService userService)
        {
            _userService = userService;
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseUser>();

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Request_Register request)
        {
            var res = await _userService.Register(request);
            switch (res.Status)
            {
                case 200:
                    return Ok(res);
                case 404:
                    return NotFound(res);
                case 400:
                    return BadRequest(res);
                case 403:
                    return Unauthorized(res);
                default:
                    return StatusCode(500, res);
            }
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] Request_ConfirmEmail request)
        {
            var res = await _userService.VerifyEmail(request);
            switch (res.Status)
            {
                case 200:
                    return Ok(res);
                case 404:
                    return NotFound(res);
                case 400:
                    return BadRequest(res);
                case 401:
                    return Unauthorized(res);
                default:
                    return StatusCode(500, res);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(Request_Login login)
        {
            return Ok(_userService.Login(login));
        }

        [HttpGet("get-all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("get-user-by-id/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userService.GetUserById(id));
        }

        [HttpPut("change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] Request_ChangePassword request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _userService.ChangePassword(id, request);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] Request_ForgotPassword request)
        {
            var result = await _userService.ForgotPassword(request);
            return Ok(result);
        }
        [HttpPut("create-new-password")]
        public async Task<IActionResult> CreateNewPassword([FromBody] Request_CreateNewPassword request)
        {
            return Ok(await _userService.CreateNewPassword(request));
        }
    }
}
