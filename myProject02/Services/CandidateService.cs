using Microsoft.EntityFrameworkCore;
using myProject02.Dto;
using myProject02.Models;
using myProject02.Services.Interfaces;

namespace myProject02.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _context;

        public CandidateService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<CandidateDto>> GetAllAsync()
        {
            return await _context.Candidates
                .Include(c => c.Party)
                .Select(c => new CandidateDto
                {
                    Id = c.Id,
                    CandidateName = c.CandidateName,
                    NIC = c.NIC,
                    PartyId = c.PartyId,
                    PartyName = c.Party.PartyName
                })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<CandidateDto?> GetByIdAsync(int id)
        {
            return await _context.Candidates
                .Include(c => c.Party)
                .Where(c => c.Id == id)
                .Select(c => new CandidateDto
                {
                    Id = c.Id,
                    CandidateName = c.CandidateName,
                    NIC = c.NIC,
                    PartyId = c.PartyId,
                    PartyName = c.Party.PartyName
                })
                .FirstOrDefaultAsync();
        }

        // GET CANDIDATES BY PARTY
        public async Task<IEnumerable<CandidateDto>> GetByPartyIdAsync(int partyId)
        {
            return await _context.Candidates
                .Include(c => c.Party)
                .Where(c => c.PartyId == partyId)
                .Select(c => new CandidateDto
                {
                    Id = c.Id,
                    CandidateName = c.CandidateName,
                    NIC = c.NIC,
                    PartyId = c.PartyId,
                    PartyName = c.Party.PartyName
                })
                .ToListAsync();
        }

        // CREATE
        public async Task<CandidateDto?> CreateAsync(
            CreateCandidateDto dto)
        {
            // Check whether party exists
            var party = await _context.Parties
                .FirstOrDefaultAsync(p => p.Id == dto.PartyId);

            if (party == null)
            {
                return null;
            }

            var candidate = new Candidate
            {
                CandidateName = dto.CandidateName,
                NIC = dto.NIC,
                PartyId = dto.PartyId
            };

            _context.Candidates.Add(candidate);

            await _context.SaveChangesAsync();

            return new CandidateDto
            {
                Id = candidate.Id,
                CandidateName = candidate.CandidateName,
                NIC = candidate.NIC,
                PartyId = candidate.PartyId,
                PartyName = party.PartyName
            };
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            UpdateCandidateDto dto)
        {
            var candidate = await _context.Candidates
                .FindAsync(id);

            if (candidate == null)
            {
                return false;
            }

            // Check new party exists
            var partyExists = await _context.Parties
                .AnyAsync(p => p.Id == dto.PartyId);

            if (!partyExists)
            {
                return false;
            }

            candidate.CandidateName = dto.CandidateName;
            candidate.NIC = dto.NIC;
            candidate.PartyId = dto.PartyId;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var candidate = await _context.Candidates
                .FindAsync(id);

            if (candidate == null)
            {
                return false;
            }

            _context.Candidates.Remove(candidate);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}