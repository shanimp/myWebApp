using Microsoft.AspNetCore.Mvc;
using myProject02.DTOs;
using myProject02.Services.Interfaces;
using myProject02.Services.Pdf;

namespace myProject02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserPdfService _pdfService;

        public UserController(
            IUserService userService,
            UserPdfService pdfService)
        {
            _userService = userService;
            _pdfService = pdfService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET: api/User/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(
            CreateUserDto dto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(dto);

                return CreatedAtAction(
                    nameof(GetUserById),
                    new { id = user.Id },
                    user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/User/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDto>> UpdateUser(
            int id,
            UpdateUserDto dto)
        {
            try
            {
                var user = await _userService.UpdateUserAsync(id, dto);

                if (user == null)
                {
                    return NotFound(new
                    {
                        message = "User not found."
                    });
                }

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/User/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            return Ok(new
            {
                message = "User deleted successfully."
            });
        }

        // GET: api/User/role/Admin
        [HttpGet("role/{roleName}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRole(
            string roleName)
        {
            var users = await _userService.GetUsersByRoleNameAsync(roleName);

            return Ok(users);
        }

        // GET: api/User/report/pdf
        [HttpGet("report/pdf")]
        public async Task<IActionResult> GenerateUserReport()
        {
            var users = (await _userService.GetAllUsersAsync())
                .ToList();

            var pdf = _pdfService.GenerateUserPdf(users);

            return File(
                pdf,
                "application/pdf",
                $"UserReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        }
    }
}