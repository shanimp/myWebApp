using myProject02.Dto;
using myProject02.Dto.ElectionParty;
namespace myProject02.Services.Interfaces
{
    public interface IElectionService
    {
        // Election management
        Task<IEnumerable<ElectionDto>> GetAllAsync();

        Task<ElectionDto?> GetByIdAsync(int id);

        Task<ElectionDto> CreateAsync(CreateElectionDto dto);

        Task<bool> UpdateAsync(int id, UpdateElectionDto dto);

        Task<bool> DeleteAsync(int id);

        // Party registration
        Task<bool> RegisterPartyAsync(
            int electionId,
            RegisterPartyDto dto);

        Task<bool> RemovePartyAsync(
            int electionId,
            int partyId);

        Task<IEnumerable<PartyDto>> GetPartiesAsync(
            int electionId);
    }
}
