using CandidateManagementSystemV2.Server.DTOs;

namespace CandidateManagementSystemV2.Server.Interfaces
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetAllCandidatesAsync();
        Task<CandidateDto> GetCandidateByIdAsync(int id);
        Task<CandidateDto> AddCandidateAsync(CandidateDto candidateDto);
        Task<CandidateDto> UpdateCandidateAsync(int id, CandidateDto candidateDto);
        Task DeleteCandidateAsync(int id);
    }
}
