using myProject02.Dto;

namespace myProject02.Services.Interfaces
{
    public interface IPartyService
    {
        Task<IEnumerable<PartyDto>> GetAllAsync();

        Task<PartyDto?> GetByIdAsync(int id);

        Task<PartyDto> CreateAsync(CreatePartyDto dto);

        Task<bool> UpdateAsync(int id, UpdatePartyDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
