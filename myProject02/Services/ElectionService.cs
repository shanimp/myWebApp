using Microsoft.EntityFrameworkCore;
using myProject02.Dto;
using myProject02.Dto.ElectionParty;
using myProject02.Models;
using myProject02.Services.Interfaces;

namespace myProject02.Services
{
    public class ElectionService : IElectionService
    {
        private readonly AppDbContext _context;

        public ElectionService(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // GET ALL ELECTIONS
        // ==========================================
        public async Task<IEnumerable<ElectionDto>> GetAllAsync()
        {
            return await _context.Elections
                .Select(e => new ElectionDto
                {
                    Id = e.Id,
                    ElectionName = e.ElectionName,
                    ElectionDate = e.ElectionDate,
                    Description = e.Description,
                    PartyCount = e.ElectionParties.Count
                })
                .ToListAsync();
        }

        // ==========================================
        // GET ELECTION BY ID
        // ==========================================
        public async Task<ElectionDto?> GetByIdAsync(int id)
        {
            return await _context.Elections
                .Where(e => e.Id == id)
                .Select(e => new ElectionDto
                {
                    Id = e.Id,
                    ElectionName = e.ElectionName,
                    ElectionDate = e.ElectionDate,
                    Description = e.Description,
                    PartyCount = e.ElectionParties.Count
                })
                .FirstOrDefaultAsync();
        }

        // ==========================================
        // CREATE ELECTION
        // ==========================================
        public async Task<ElectionDto> CreateAsync(
            CreateElectionDto dto)
        {
            var election = new Election
            {
                ElectionName = dto.ElectionName,
                ElectionDate = dto.ElectionDate,
                Description = dto.Description
            };

            _context.Elections.Add(election);

            await _context.SaveChangesAsync();

            return new ElectionDto
            {
                Id = election.Id,
                ElectionName = election.ElectionName,
                ElectionDate = election.ElectionDate,
                Description = election.Description,
                PartyCount = 0
            };
        }

        // ==========================================
        // UPDATE ELECTION
        // ==========================================
        public async Task<bool> UpdateAsync(
            int id,
            UpdateElectionDto dto)
        {
            var election = await _context.Elections
                .FindAsync(id);

            if (election == null)
            {
                return false;
            }

            election.ElectionName = dto.ElectionName;
            election.ElectionDate = dto.ElectionDate;
            election.Description = dto.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        // ==========================================
        // DELETE ELECTION
        // ==========================================
        public async Task<bool> DeleteAsync(int id)
        {
            var election = await _context.Elections
                .Include(e => e.ElectionParties)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (election == null)
            {
                return false;
            }

            _context.Elections.Remove(election);

            await _context.SaveChangesAsync();

            return true;
        }

        // ==========================================
        // REGISTER PARTY FOR ELECTION
        // ==========================================
        public async Task<bool> RegisterPartyAsync(
            int electionId,
            RegisterPartyDto dto)
        {
            // Check election
            var electionExists = await _context.Elections
                .AnyAsync(e => e.Id == electionId);

            if (!electionExists)
            {
                return false;
            }

            // Check party
            var partyExists = await _context.Parties
                .AnyAsync(p => p.Id == dto.PartyId);

            if (!partyExists)
            {
                return false;
            }

            // Check duplicate registration
            var alreadyRegistered =
                await _context.ElectionParties
                    .AnyAsync(ep =>
                        ep.ElectionId == electionId &&
                        ep.PartyId == dto.PartyId);

            if (alreadyRegistered)
            {
                return false;
            }

            var electionParty = new ElectionParty
            {
                ElectionId = electionId,
                PartyId = dto.PartyId
            };

            _context.ElectionParties.Add(electionParty);

            await _context.SaveChangesAsync();

            return true;
        }

        // ==========================================
        // REMOVE PARTY FROM ELECTION
        // ==========================================
        public async Task<bool> RemovePartyAsync(
            int electionId,
            int partyId)
        {
            var electionParty =
                await _context.ElectionParties
                    .FirstOrDefaultAsync(ep =>
                        ep.ElectionId == electionId &&
                        ep.PartyId == partyId);

            if (electionParty == null)
            {
                return false;
            }

            _context.ElectionParties.Remove(electionParty);

            await _context.SaveChangesAsync();

            return true;
        }

        // ==========================================
        // GET PARTIES REGISTERED FOR ELECTION
        // ==========================================
        public async Task<IEnumerable<PartyDto>> GetPartiesAsync(
            int electionId)
        {
            return await _context.ElectionParties
                .Where(ep => ep.ElectionId == electionId)
                .Select(ep => new PartyDto
                {
                    Id = ep.Party.Id,
                    PartyName = ep.Party.PartyName,
                    PartySymbol = ep.Party.PartySymbol,
                    CandidateCount = ep.Party.Candidates.Count
                })
                .ToListAsync();
        }
    }
}