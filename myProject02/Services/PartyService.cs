using Microsoft.EntityFrameworkCore;
using myProject02.Dto;
using myProject02.Models;
using myProject02.Services.Interfaces;

namespace myProject02.Services
{
    public class PartyService : IPartyService
    {
        private readonly AppDbContext _context;

        public PartyService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<PartyDto>> GetAllAsync()
        {
            return await _context.Parties
                .Select(p => new PartyDto
                {
                    Id = p.Id,
                    PartyName = p.PartyName,
                    PartySymbol = p.PartySymbol,
                    CandidateCount = p.Candidates.Count
                })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<PartyDto?> GetByIdAsync(int id)
        {
            return await _context.Parties
                .Where(p => p.Id == id)
                .Select(p => new PartyDto
                {
                    Id = p.Id,
                    PartyName = p.PartyName,
                    PartySymbol = p.PartySymbol,
                    CandidateCount = p.Candidates.Count
                })
                .FirstOrDefaultAsync();
        }

        // CREATE
        public async Task<PartyDto> CreateAsync(CreatePartyDto dto)
        {
            var party = new Party
            {
                PartyName = dto.PartyName,
                PartySymbol = dto.PartySymbol
            };

            _context.Parties.Add(party);

            await _context.SaveChangesAsync();

            return new PartyDto
            {
                Id = party.Id,
                PartyName = party.PartyName,
                PartySymbol = party.PartySymbol,
                CandidateCount = 0
            };
        }

        // UPDATE
        public async Task<bool> UpdateAsync(int id, UpdatePartyDto dto)
        {
            var party = await _context.Parties
                .FindAsync(id);

            if (party == null)
            {
                return false;
            }

            party.PartyName = dto.PartyName;
            party.PartySymbol = dto.PartySymbol;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var party = await _context.Parties
                .Include(p => p.Candidates)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (party == null)
            {
                return false;
            }

            // Don't delete party if it has candidates
            if (party.Candidates.Any())
            {
                return false;
            }

            _context.Parties.Remove(party);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
