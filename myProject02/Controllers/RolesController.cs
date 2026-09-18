using Microsoft.AspNetCore.Mvc;
using myProject02.Dto;
using myProject02.Services;


namespace myProject02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController: ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllRoles()
        //{
           // var roles = await _roleService.GetAllRolesAsync();
            //return Ok(roles);
       //}

        [HttpPost]
        public  async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdRole = await _roleService.CreateRoleAsync(dto);
            if (createdRole == null)
            {
                return BadRequest("A role with this name already exists.");
            }

            return CreatedAtAction(nameof(GetRoleById), new {id = createdRole.Id},
                createdRole);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound("Role not found.");

            return Ok(role);
        }

    }
}
