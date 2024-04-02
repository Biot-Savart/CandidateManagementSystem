using AutoMapper;
using CandidateManagementSystemV2.Server.Data;
using CandidateManagementSystemV2.Server.DTOs;
using CandidateManagementSystemV2.Server.Interfaces;
using CandidateManagementSystemV2.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystemV2.Server.Services
{
    public class PositionService : IPositionService
    {
        private readonly CandidateDBContext _context;
        private readonly IMapper _mapper;

        public PositionService(CandidateDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PositionDto>> GetAllPositionsAsync()
        {
            var positions = await _context.Positions.Where(c => c.Archived == null).ToListAsync();
            return _mapper.Map<IEnumerable<PositionDto>>(positions);
        }

        public async Task<PositionDto> GetCPositionByIdAsync(int id)
        {
            var position = await _context.Positions.FirstOrDefaultAsync(c => c.PositionId == id);


            if (position == null)
            {
                throw new KeyNotFoundException($"Position with ID {id} not found.");
            }

            return _mapper.Map<PositionDto>(position);
        }
    }
}
