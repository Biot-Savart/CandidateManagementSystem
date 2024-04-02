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

            candidate.Archived = DateTime.Now.ToUniversalTime();

            _context.Entry(candidate).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        async Task<CandidateDto> ICandidateService.UpdateCandidateAsync(int id, CandidateDto candidateDto)
        {
            var existingCandidate = await _context.Candidates.Include(c => c.Skills).Include(c => c.CandidatePositions).FirstOrDefaultAsync(c => c.CandidateId == id);
            if (existingCandidate == null)
            {
                throw new KeyNotFoundException($"Candidate with ID {id} not found.");
            }
            
            // Handle the Skills collection
            foreach (var existingSkill in existingCandidate.Skills)
            {
                var newSkill = candidateDto.Skills?.FirstOrDefault(s => s.SkillId == existingSkill.SkillId);
                if (newSkill == null)
                    _context.Skills.Remove(existingSkill); // Skill was removed
                else
                    _context.Entry(existingSkill).CurrentValues.SetValues(newSkill); // Update existing skill
            }

            // Add new skills
            foreach (var newSkill in candidateDto.Skills)
            {
                if (!existingCandidate.Skills.Any(s => s.SkillId == newSkill.SkillId))
                {
                    Skill skill = new Skill { 
                        Name = newSkill.Name, 
                        YearsOfExperience= newSkill.YearsOfExperience,
                        CandidateId = newSkill.CandidateId,
                        //Created = DateTime.Now,
                    };
                    // New skill, need to set it up correctly in EF Core
                    existingCandidate.Skills.Add(skill);
                }
            }

            // First, remove unselected positions
            var candidatePositions = existingCandidate.CandidatePositions?.ToList();
            if (candidatePositions != null && candidateDto.CandidatePositions != null)
            {
                foreach (var position in candidatePositions)
                {
                    if (!candidateDto.CandidatePositions.Any(p => p.PositionId == position.PositionId))
                    {
                        existingCandidate.CandidatePositions?.Remove(position);
                    }
                }
            }

            if (candidateDto.CandidatePositions != null)
            {
                // Then, add new positions
                foreach (var positionDto in candidateDto.CandidatePositions)
                {
                    if (!existingCandidate.CandidatePositions.Any(p => p.PositionId == positionDto.PositionId))
                    {
                        var position = await _context.Positions.FindAsync(positionDto.PositionId);
                        CandidatePosition newCandidatePosition = new CandidatePosition
                        {
                            CandidateId = 1,
                            PositionId = 2,
                        };

                        if (newCandidatePosition != null)
                        {
                            existingCandidate.CandidatePositions.Add(newCandidatePosition);
                        }
                        else
                        {
                            // Handle the case where a position ID from the DTO does not exist in the database
                            // This might involve logging a warning, throwing an exception, or ignoring the entry
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<CandidateDto>(existingCandidate);
        }
    }
}
