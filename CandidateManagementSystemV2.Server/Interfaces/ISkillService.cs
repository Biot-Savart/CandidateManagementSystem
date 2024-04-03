using CandidateManagementSystemV2.Server.DTOs;

namespace CandidateManagementSystemV2.Server.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDto>> GetAllSkillsAsync();
        Task<SkillDto> GetSkillByIdAsync(int id);

        Task<IEnumerable<SkillCandidateCountDto>> GetSkillCandidateCount();
    }
}
