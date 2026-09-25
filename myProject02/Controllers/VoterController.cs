using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myProject02.Dto;
using myProject02.Models;
using myProject02.Services;
using myProject02.Services.Pdf;

namespace myProject02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoterController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly VoterPdfService _pdfService;
        private readonly Ivoterservice _voterService;

        public VoterController(
            AppDbContext context,
            VoterPdfService pdfService,
            Ivoterservice voterService)
        {
            _context = context;
            _pdfService = pdfService;
            _voterService = voterService;
        }

        // GET: api/Voter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoterDTO>>> GetVoters()
        {
            var voters = await _voterService.GetAllVotersAsync();

            return Ok(voters);
        }

        // GET: api/Voter/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VoterDTO>> GetVoter(int id)
        {
            var voterDto = await _voterService.GetVoterByIdAsync(id);

            if (voterDto is null)
            {
                return NotFound();
            }

            return Ok(voterDto);
        }

        // POST: api/Voter
        [HttpPost]
        public async Task<ActionResult<VoterDTO>> CreateVoter(
            CreateVoterDTO createVoterDto)
        {
            var createdVoter =
                await _voterService.CreateVoterAsync(createVoterDto);

            return CreatedAtAction(
                nameof(GetVoter),
                new { id = createdVoter.Id },
                createdVoter);
        }

        // GET: api/Voter/report/pdf
        [HttpGet("report/pdf")]
        public async Task<IActionResult> GenerateVoterReport()
        {
            var voters = await _context.Voters
                .OrderBy(v => v.Id)
                .ToListAsync();

            var pdf = _pdfService.GenerateVoterPdf(voters);

            return File(
                pdf,
                "application/pdf",
                $"VoterReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        }
    }
}