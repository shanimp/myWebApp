using myProject02.Dto;

namespace myProject02.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetAllAsync();

        Task<CandidateDto?> GetByIdAsync(int id);

        Task<IEnumerable<CandidateDto>> GetByPartyIdAsync(int partyId);

        Task<CandidateDto?> CreateAsync(CreateCandidateDto dto);

        Task<bool> UpdateAsync(int id, UpdateCandidateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
