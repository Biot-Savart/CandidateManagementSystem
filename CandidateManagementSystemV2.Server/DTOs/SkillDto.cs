namespace CandidateManagementSystemV2.Server.DTOs
{
    public class SkillDto
    {
        public int CandidateId { get; set; }
        public int SkillId { get; set; }
        public required string Name { get; set; }
        public int YearsOfExperience { get; set; }
    }

    public class SkillCandidateCountDto
    {
        public required string Name { get; set; }
        public int CandidateCount { get; set; }
    }
}
