using Microsoft.AspNetCore.Mvc;
using myProject02.Dto;
using myProject02.Services;

namespace myProject02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class VoterController : ControllerBase
    {
        private readonly Ivoterservice _voterService;
        //private readonly IPdfService _pdfService;
        public VoterController(Ivoterservice voterService)
        {
            _voterService = voterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoterDTO>>> GetVoters()
            => Ok(await _voterService.GetAllVotersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<VoterDTO>> GetVoter(int id)
        {
            var voterDto = await _voterService.GetVoterByIdAsync(id);
            return voterDto is null ? NotFound() : Ok(voterDto);
        }


        [HttpPost]
        public async Task<ActionResult<VoterDTO>> CreateVoter(CreateVoterDTO createVoterDto)
        {
            var createdVoter = await _voterService.CreateVoterAsync(createVoterDto);
            return CreatedAtAction(nameof(GetVoter), new { id = createdVoter.Id }, createdVoter);
        }
    }
}
