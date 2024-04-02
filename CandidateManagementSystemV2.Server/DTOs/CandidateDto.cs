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

    public class SkillDto
    {
        public int CandidateId { get; set; }
        public int SkillId { get; set; }
        public required string Name { get; set; }
        public int YearsOfExperience { get; set; }
    }

    public class CandidatePositionDto
    {
        public int CandidateId { get; set; }
        public int PositionId { get; set; }
        public PositionDto? Position { get; set; }
    }

    public class PositionDto
    {
        public int PositionId { get; set; }
        public string Title { get; set; }
    }

}
