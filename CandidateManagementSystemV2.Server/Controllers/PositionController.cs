using CandidateManagementSystemV2.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CandidateManagementSystemV2.Server.Models;
using CandidateManagementSystemV2.Server.Interfaces;
using CandidateManagementSystemV2.Server.Services;

namespace CandidateManagementSystemV2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            var poitionsDto = await _positionService.GetAllPositionsAsync();

            return Ok(poitionsDto);
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            var poitionDto = await _positionService.GetCPositionByIdAsync(id);

            return Ok(poitionDto);
        }
    }
}
