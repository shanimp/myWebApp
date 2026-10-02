using Microsoft.AspNetCore.Mvc;
using myProject02.Dto;
using myProject02.Dto.ElectionParty;
using myProject02.Services.Interfaces;

namespace myProject02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectionController : ControllerBase
    {
        private readonly IElectionService _electionService;

        public ElectionController(
            IElectionService electionService)
        {
            _electionService = electionService;
        }

        // ==========================================
        // GET: api/Election
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var elections =
                await _electionService.GetAllAsync();

            return Ok(elections);
        }

        // ==========================================
        // GET: api/Election/1
        // ==========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var election =
                await _electionService.GetByIdAsync(id);

            if (election == null)
            {
                return NotFound(new
                {
                    message = "Election not found."
                });
            }

            return Ok(election);
        }

        // ==========================================
        // POST: api/Election
        // ==========================================
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateElectionDto dto)
        {
            var election =
                await _electionService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = election.Id },
                election
            );
        }

        // ==========================================
        // PUT: api/Election/1
        // ==========================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateElectionDto dto)
        {
            var result =
                await _electionService.UpdateAsync(id, dto);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Election not found."
                });
            }

            return Ok(new
            {
                message = "Election updated successfully."
            });
        }

        // ==========================================
        // DELETE: api/Election/1
        // ==========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _electionService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Election not found."
                });
            }

            return Ok(new
            {
                message = "Election deleted successfully."
            });
        }

        // ==========================================
        // GET PARTIES
        // GET: api/Election/1/parties
        // ==========================================
        [HttpGet("{electionId}/parties")]
        public async Task<IActionResult> GetParties(
            int electionId)
        {
            var election =
                await _electionService.GetByIdAsync(electionId);

            if (election == null)
            {
                return NotFound(new
                {
                    message = "Election not found."
                });
            }

            var parties =
                await _electionService.GetPartiesAsync(electionId);

            return Ok(parties);
        }

        // ==========================================
        // REGISTER PARTY
        // POST: api/Election/1/parties
        // ==========================================
        [HttpPost("{electionId}/parties")]
        public async Task<IActionResult> RegisterParty(
            int electionId,
            [FromBody] RegisterPartyDto dto)
        {
            var result =
                await _electionService.RegisterPartyAsync(
                    electionId,
                    dto);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Election or party does not exist, " +
                        "or party is already registered."
                });
            }

            return Ok(new
            {
                message =
                    "Party registered for election successfully."
            });
        }

        // ==========================================
        // REMOVE PARTY
        // DELETE: api/Election/1/parties/2
        // ==========================================
        [HttpDelete("{electionId}/parties/{partyId}")]
        public async Task<IActionResult> RemoveParty(
            int electionId,
            int partyId)
        {
            var result =
                await _electionService.RemovePartyAsync(
                    electionId,
                    partyId);

            if (!result)
            {
                return NotFound(new
                {
                    message =
                        "Party registration not found."
                });
            }

            return Ok(new
            {
                message =
                    "Party removed from election successfully."
            });
        }
    }
}