using AutoMapper;
using CandidateManagementSystemV2.Server.Data;
using CandidateManagementSystemV2.Server.DTOs;
using CandidateManagementSystemV2.Server.Interfaces;
using CandidateManagementSystemV2.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystemV2.Server.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CandidateDBContext _context;
        private readonly IMapper _mapper;

        public CandidateService(CandidateDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CandidateDto>> GetAllCandidatesAsync()
        {
            var candidates = await _context.Candidates.Include(c => c.Skills).Include(c => c.CandidatePositions).ThenInclude(cp => cp.Position).Where(c => c.Archived == null).ToListAsync();
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }

        async Task<CandidateDto> ICandidateService.GetCandidateByIdAsync(int id)
        {
            var candidate = await _context.Candidates.Include(c => c.Skills).Include(c => c.CandidatePositions).ThenInclude(cp => cp.Position).FirstOrDefaultAsync(c => c.CandidateId == id);

            if (candidate == null)
            {
                throw new KeyNotFoundException($"Candidate with ID {id} not found.");
            }

            return _mapper.Map<CandidateDto>(candidate);
        }

        public async Task<CandidateDto> AddCandidateAsync(CandidateDto candidateDto)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return _mapper.Map<CandidateDto>(candidate);
        }

        async Task ICandidateService.DeleteCandidateAsync(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id) ?? throw new KeyNotFoundException($"Candidate with ID {id} not found.");

            if (candidate == null)
            {
                throw new KeyNotFoundException($"Candidate with ID {id} not found.");
            }

            candidate.Archived = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        async Task<CandidateDto> ICandidateService.UpdateCandidateAsync(int id, CandidateDto candidateDto)
        {
            var existingCandidate = await GetCandidateOrThrowAsync(id);

            _mapper.Map(candidateDto, existingCandidate); // Assuming this maps basic properties

            await UpdateSkills(existingCandidate.Skills, candidateDto.Skills);
            await UpdateCandidatePositions(existingCandidate, candidateDto.CandidatePositions);

            await _context.SaveChangesAsync();

            return _mapper.Map<CandidateDto>(existingCandidate);
        }

        private async Task<Candidate> GetCandidateOrThrowAsync(int candidateId)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Skills)
                .Include(c => c.CandidatePositions)
                .FirstOrDefaultAsync(c => c.CandidateId == candidateId && c.Archived == null);

            if (candidate == null)
            {
                throw new KeyNotFoundException($"Candidate with ID {candidateId} not found.");
            }

            return candidate;
        }

        private async Task UpdateSkills(IEnumerable<Skill> existingSkills, IEnumerable<SkillDto> newSkillsDto)
        {
            // Assuming SkillDto is your DTO class for Skill entity
            var newSkills = _mapper.Map<IEnumerable<Skill>>(newSkillsDto);

            // Remove skills not present in the new list
            foreach (var existingSkill in existingSkills.ToList())
            {
                if (!newSkills.Any(s => s.SkillId == existingSkill.SkillId))
                {
                    _context.Skills.Remove(existingSkill);
                }
            }

            // Add or update skills
            foreach (var newSkill in newSkills)
            {
                var existingSkill = existingSkills.FirstOrDefault(s => s.SkillId == newSkill.SkillId);
                if (existingSkill != null)
                {
                    _context.Entry(existingSkill).CurrentValues.SetValues(newSkill);
                }
                else
                {
                    // Assuming you have a Candidate reference in your Skill entity to set this up correctly
                    existingSkills.ToList().Add(newSkill);
                }
            }
        }

        private async Task UpdateCandidatePositions(Candidate existingCandidate, IEnumerable<CandidatePositionDto> newPositionDtos)
        {
            var newPositionIds = newPositionDtos.Select(np => np.PositionId).Distinct().ToList();
            var existingPositionIds = existingCandidate.CandidatePositions.Select(cp => cp.PositionId).ToList();

            var positionIdsToAdd = newPositionIds.Except(existingPositionIds).ToList();
            var positionIdsToRemove = existingPositionIds.Except(newPositionIds).ToList();

            // Fetch all relevant positions in a single query
            var positionsToAdd = await _context.Positions
                                       .Where(p => positionIdsToAdd.Contains(p.PositionId))
                                       .ToListAsync();

            foreach (var positionIdToRemove in positionIdsToRemove)
            {
                var positionToRemove = existingCandidate.CandidatePositions.FirstOrDefault(cp => cp.PositionId == positionIdToRemove);
                if (positionToRemove != null)
                {
                    _context.CandidatePositions.Remove(positionToRemove);
                }
            }

            foreach (var position in positionsToAdd)
            {
                existingCandidate.CandidatePositions.Add(new CandidatePosition
                {
                    CandidateId = existingCandidate.CandidateId,
                    PositionId = position.PositionId
                });
            }
        
        }

    }
}
