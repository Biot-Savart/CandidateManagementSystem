using AutoMapper;
using CandidateManagementSystemV2.Server.Data;
using CandidateManagementSystemV2.Server.DTOs;
using CandidateManagementSystemV2.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystemV2.Server.Services
{
    public class SkillService : ISkillService
    {
        private readonly CandidateDBContext _context;
        private readonly IMapper _mapper;

        public SkillService(CandidateDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillDto>> GetAllSkillsAsync()
        {
            var skills = await _context.Skills.Where(c => c.Archived == null).ToListAsync();
            return _mapper.Map<IEnumerable<SkillDto>>(skills);
        }

        public async Task<IEnumerable<SkillCandidateCountDto>> GetSkillCandidateCount()
        {
            var reportData = await _context.Skills.Where(c => c.Archived == null)
                .GroupBy(c => c.Name)
                .Select(s => new SkillCandidateCountDto
                {
                    Name = s.Key,
                    CandidateCount = s.Count(),
                })
                .ToListAsync();

            return _mapper.Map<IEnumerable<SkillCandidateCountDto>>(reportData);
        }

        public async Task<SkillDto> GetSkillByIdAsync(int id)
        {
            var skill = await _context.Skills.FirstOrDefaultAsync(c => c.SkillId == id);


            if (skill == null)
            {
                throw new KeyNotFoundException($"Skill with ID {id} not found.");
            }

            return _mapper.Map<SkillDto>(skill);
        }
    }
}
