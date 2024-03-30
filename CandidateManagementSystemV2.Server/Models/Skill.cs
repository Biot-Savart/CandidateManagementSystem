namespace CandidateManagementSystemV2.Server.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public required string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Archived { get; set; }

        // Foreign key for the Candidate this skill belongs to
        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; } // Navigation property
    }
}
