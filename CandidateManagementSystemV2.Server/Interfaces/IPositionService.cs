using CandidateManagementSystemV2.Server.DTOs;

namespace CandidateManagementSystemV2.Server.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionDto>> GetAllPositionsAsync();
        Task<PositionDto> GetCPositionByIdAsync(int id);
    }
}
