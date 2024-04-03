using CandidateManagementSystemV2.Server.DTOs;
using CandidateManagementSystemV2.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CandidateManagementSystemV2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills()
        {
            var poitionsDto = await _skillService.GetAllSkillsAsync();

            return Ok(poitionsDto);
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDto>> GetSkill(int id)
        {
            var poitionDto = await _skillService.GetSkillByIdAsync(id);

            return Ok(poitionDto);
        }

        [HttpGet("skill-candidates-count")]
        public async Task<ActionResult<IEnumerable<SkillCandidateCountDto>>> GetSkillCandidateCount()
        {
            var reportData = await _skillService.GetSkillCandidateCount();

            return Ok(reportData);
        }
    }
}
