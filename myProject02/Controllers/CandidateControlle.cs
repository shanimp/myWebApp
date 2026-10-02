using Microsoft.AspNetCore.Mvc;
using myProject02.Dto;
using myProject02.Services.Interfaces;

namespace myProject02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _candidateService.GetAllAsync();

            return Ok(candidates);
        }

        // GET: api/Candidate/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var candidate = await _candidateService.GetByIdAsync(id);

            if (candidate == null)
            {
                return NotFound(new
                {
                    message = "Candidate not found."
                });
            }

            return Ok(candidate);
        }

        // GET: api/Candidate/party/1
        [HttpGet("party/{partyId}")]
        public async Task<IActionResult> GetByPartyId(int partyId)
        {
            var candidates =
                await _candidateService.GetByPartyIdAsync(partyId);

            return Ok(candidates);
        }

        // POST: api/Candidate
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateCandidateDto dto)
        {
            var candidate =
                await _candidateService.CreateAsync(dto);

            if (candidate == null)
            {
                return BadRequest(new
                {
                    message = "The specified party does not exist."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = candidate.Id },
                candidate
            );
        }

        // PUT: api/Candidate/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateCandidateDto dto)
        {
            var result =
                await _candidateService.UpdateAsync(id, dto);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Candidate not found or specified party does not exist."
                });
            }

            return Ok(new
            {
                message = "Candidate updated successfully."
            });
        }

        // DELETE: api/Candidate/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _candidateService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Candidate not found."
                });
            }

            return Ok(new
            {
                message = "Candidate deleted successfully."
            });
        }
    }
}
