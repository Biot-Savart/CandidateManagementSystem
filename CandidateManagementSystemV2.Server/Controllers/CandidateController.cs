using CandidateManagementSystemV2.Server.Data;
using CandidateManagementSystemV2.Server.DTOs;
using CandidateManagementSystemV2.Server.Interfaces;
using CandidateManagementSystemV2.Server.Models;
using CandidateManagementSystemV2.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystemV2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDto>>> GetCandidates()
        {
            var candidatesDto = await _candidateService.GetAllCandidatesAsync();

            return Ok(candidatesDto);
        }

        // GET: api/Candidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDto>> GetCandidate(int id)
        {
            var candidateDto = await _candidateService.GetCandidateByIdAsync(id);

            return Ok(candidateDto);
        }

        // POST: api/Candidate
        [HttpPost]
        public async Task<ActionResult<CandidateDto>> PostCandidate(CandidateDto candidate)
        {
            await _candidateService.AddCandidateAsync(candidate);

            return CreatedAtAction("GetCandidate", new { id = candidate.CandidateId }, candidate);
        }

        // PUT: api/Candidate/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CandidateDto>> PutCandidate(int id, CandidateDto candidate)
        {
            if (id != candidate.CandidateId)
            {
                return BadRequest("The ID provided in the path does not match the CandidateId in the payload.");
            }

            try
            {
                return await _candidateService.UpdateCandidateAsync(id, candidate);
            }
            catch (KeyNotFoundException ex)
            {
                // Handle case where candidate doesn't exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details here as needed
                return StatusCode(500, "An error occurred while updating the candidate. " + ex.Message);
            }
        }


        // DELETE: api/Candidate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            try
            {
                await _candidateService.DeleteCandidateAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Handle case where candidate doesn't exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details here as needed
                return StatusCode(500, "An error occurred while deleting the candidate. " + ex.Message);
            }
        }
    }
}
