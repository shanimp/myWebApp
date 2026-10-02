using Microsoft.AspNetCore.Mvc;
using myProject02.Dto;
using myProject02.Services.Interfaces;

namespace myProject02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartyController : ControllerBase
    {
        private readonly IPartyService _partyService;

        public PartyController(IPartyService partyService)
        {
            _partyService = partyService;
        }

        // GET: api/Party
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var parties = await _partyService.GetAllAsync();

            return Ok(parties);
        }

        // GET: api/Party/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var party = await _partyService.GetByIdAsync(id);

            if (party == null)
            {
                return NotFound(new
                {
                    message = "Party not found."
                });
            }

            return Ok(party);
        }

        // POST: api/Party
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreatePartyDto dto)
        {
            var party = await _partyService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = party.Id },
                party
            );
        }

        // PUT: api/Party/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdatePartyDto dto)
        {
            var result = await _partyService.UpdateAsync(id, dto);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Party not found."
                });
            }

            return Ok(new
            {
                message = "Party updated successfully."
            });
        }

        // DELETE: api/Party/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _partyService.DeleteAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Party not found or party has candidates."
                });
            }

            return Ok(new
            {
                message = "Party deleted successfully."
            });
        }
    }
}