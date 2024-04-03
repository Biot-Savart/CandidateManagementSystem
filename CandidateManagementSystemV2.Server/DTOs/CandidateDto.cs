namespace CandidateManagementSystemV2.Server.DTOs
{
    public class CandidateDto
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Experience { get; set; }
        public List<SkillDto>? Skills { get; set; }
        public List<CandidatePositionDto>? CandidatePositions { get; set; }
    }
}
